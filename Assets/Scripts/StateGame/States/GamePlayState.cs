using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : State
{
    public event Action OnGame;
    
    private GameStateManager stateManager;
    
    public GamePlayState(GameStateManager gameStateManager)
    {
        stateManager = gameStateManager;
    }
    public override void Enter()
    {
        stateManager.isGame = true;
        OnGame?.Invoke();
    }

    public override void Exit()
    {   
        stateManager.isGame = false;
        //Debug.Log(isGame);
    }

    public override void Update()
    {
        //Debug.Log(isGame);
    }
}
