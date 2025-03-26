using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Gameplay,
    UI
}

public class GameStateManager : MonoBehaviour
{
    private GameState gameState;
    
    public GameState GameState => gameState;
    
    private static GameStateManager _instance;

    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameStateManager>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void ChangeState(GameState newState)
    {
        gameState = newState;
    }
}
