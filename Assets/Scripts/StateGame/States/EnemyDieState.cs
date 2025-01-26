using DG.Tweening;
using System;
using UnityEngine;

public class EnemyDieState : State
{
    public event Action OnEndDie;
    private GameStateManager stateManager;
    private Vector3 endPosition = new Vector3(0,-10,0);
    private float durationMove = 1;
    public EnemyDieState(GameStateManager gameStateManager)
    {
        stateManager = gameStateManager;
    }
    public override void Enter()
    {
        stateManager.isGame = false;
        stateManager.groundManager.StopedMove(true);
        GameObject currentEnemy = stateManager.enemyManager.CurrentEnemy;
        currentEnemy.transform.DOMove(endPosition, durationMove)
            .SetEase(Ease.InFlash)
            .OnComplete(() =>
            {
                OnEndDie?.Invoke();
                Debug.Log("enter enemy die");
            });
        
    }

    public override void Exit()
    {
        Debug.Log("exit enemy die");
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
