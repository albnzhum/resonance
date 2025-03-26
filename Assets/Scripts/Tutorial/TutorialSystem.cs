using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TutorialStages
{
    Movement,
    WaterRipple,
    Leaves,
    Fire,
    
}

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private List<Tutorial> _tutorialStages;
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject _player;
    
    [SerializeField] private GameObject startWindow;

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

        _tutorialStages[currentStageIndex].Show();
        _tutorialStages[currentStageIndex].OnStageCompleted += OnCurrentStageCompleted;

        if (currentAction != null)
        {
            currentAction.OnActionCompleted += OnActionCompleted;
            currentAction.StartAction();
        }
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
                return _player.GetComponent<MoveAction>();
            case TutorialStages.Leaves:
                return _player.GetComponent<LeavesAction>();
            case TutorialStages.WaterRipple:
                return _player.GetComponent<WaterRippleAction>();
            default:
                return null;
        }
    }
}