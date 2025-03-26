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

    private GameStateManager gameStateManager;

    private int currentStageIndex = 0;
    private bool isCompleted = false;

    private TutorialStages currentStage;
    
    public TutorialStages CurrentStage => currentStage;

    private void Start()
    {
        gameStateManager = GameStateManager.Instance;
        
        StartTutorial(true);
    }

    public void SkipTutorial()
    {
        isCompleted = true;
        EndTutorial();
    }

    private void StartTutorial(bool show)
    {
        _tutorial.SetActive(show);

        gameStateManager.ChangeState(GameState.UI);
        ShowCurrentStage();
    }

    private void EndTutorial()
    {
        gameStateManager.ChangeState(GameState.Gameplay);
        _tutorial.SetActive(false);
    }

    private void ShowCurrentStage()
    {
        if (currentStageIndex < _tutorialStages.Count)
        {
            _tutorialStages[currentStageIndex].Show();
            _tutorialStages[currentStageIndex].OnStageCompleted += OnCurrentStageCompleted;
        }
        else
        {
            EndTutorial();
        }
    }

    private void OnCurrentStageCompleted()
    {
        _tutorialStages[currentStageIndex].OnStageCompleted -= OnCurrentStageCompleted;
        _tutorialStages[currentStageIndex].Close();
        currentStageIndex++;

        // Show the next stage
        ShowCurrentStage();
    }
}