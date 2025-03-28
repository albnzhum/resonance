using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialStages
{
    Movement,
    Light,
    MoveTo,
    WaterLight,
    WaterRipple,
    WaterActivation,
    FindBird,
    LeavesSound,
    Empty,
    
    Fire,
    
}

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private List<Tutorial> _tutorialStages;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject player;
    
    [SerializeField] private GameObject startWindow;
    [SerializeField] private CameraController cameraController;

    [Header("Actions")] 
    [SerializeField] private MoveAction moveAction;
    [SerializeField] private FocusTrigger lightAction;
    [SerializeField] private WaterLightAction waterLightAction;
    [SerializeField] private FocusTriggerExit moveToLightAction;
    [SerializeField] private FocusTrigger levelFocusTrigger;
    [SerializeField] private FocusTriggerExit levelExitTrigger;
    [SerializeField] private FocusTrigger leavesTrigger;
    [SerializeField] private FocusTriggerExit leavesExitTrigger;


    private GameStateManager gameStateManager;
    
    private int currentStageIndex = 0;
    private bool isCompleted = false;
    
    private ITutorialAction currentAction;

    private TutorialStages currentStage;

    public TutorialStages CurrentStage => currentStage;

    private void Start()
    {
        gameStateManager = GameStateManager.Instance;

        _tutorial.SetActive(true);
        gameStateManager.ChangeState(GameState.UI);
    }

    public void SkipTutorial()
    {
        isCompleted = true;
        EndTutorial();
    }

    public void StartTutorial()
    {
        startWindow.SetActive(false);
        gameStateManager.ChangeState(GameState.Gameplay);
        
        cameraController.ActivePlayerCamera();

        ShowCurrentStage();
    }

    private void EndTutorial()
    {
        gameStateManager.ChangeState(GameState.Gameplay);
        _tutorial.SetActive(false);
    }

    private void ShowCurrentStage()
    {
        if (currentStageIndex >= _tutorialStages.Count)
        {
            isCompleted = true;
            EndTutorial();
            return;
        }

        currentStage = (TutorialStages)currentStageIndex;

        currentAction = GetActionForStage(currentStage);
        if (currentAction == null)
        {
            Debug.LogError($"Action not found for stage {currentStage}!");
            return;
        }

        _tutorialStages[currentStageIndex].Show();
        _tutorialStages[currentStageIndex].OnStageCompleted += OnCurrentStageCompleted;

        currentAction.OnActionCompleted += OnActionCompleted;
        currentAction.StartAction();
    }

    private void OnCurrentStageCompleted()
    {
        if (currentAction != null)
        {
            currentAction.OnActionCompleted -= OnActionCompleted;
        }

        _tutorialStages[currentStageIndex].OnStageCompleted -= OnCurrentStageCompleted;
        _tutorialStages[currentStageIndex].Close();
        currentStageIndex++;

        ShowCurrentStage();
    }

    private void OnActionCompleted()
    {
        OnCurrentStageCompleted();
    }
    

    private ITutorialAction GetActionForStage(TutorialStages stage)
    {
        switch (stage)
        {
            case TutorialStages.Movement:
                return moveAction;
            case TutorialStages.Light:
                return lightAction;
            case TutorialStages.MoveTo:
                return moveToLightAction;
            case TutorialStages.WaterLight:
                return waterLightAction;
            case TutorialStages.WaterRipple:
                return player.GetComponent<WaterRippleAction>();
            case TutorialStages.WaterActivation:
                return levelFocusTrigger;
            case TutorialStages.FindBird:
                return levelExitTrigger;
            case TutorialStages.LeavesSound:
                return leavesTrigger;
            case TutorialStages.Empty:
                return leavesExitTrigger;
            default:
                return null;
        }
    }
}