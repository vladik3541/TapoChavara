using UnityEngine;
using DG.Tweening;
using System;

public class SpawnEnemyState : State
{
    private GameStateManager stateManager;
    private Interstitial _interstitial;
    private float duration = 1;

    private int coutFromAds = 0;
    const int delayAds = 2;

    public SpawnEnemyState(GameStateManager gameStateManager, Interstitial interstitial) 
    {
        stateManager = gameStateManager;
        _interstitial = interstitial;
    }
    public override void Enter()
    {
        stateManager.isGame = true;
        GameObject enemy = stateManager.enemyManager.Spawn();
        stateManager.uiManager.UpdateLevel(stateManager.enemyManager.EnemyLevel);
        StartBehavior(enemy);
        coutFromAds++;
        if(coutFromAds>= 2)
        {
            coutFromAds = 0;
            _interstitial.ShowAd();
        }
    }

    public override void Exit()
    {
        stateManager.groundManager.StopedMove(false);
    }

    public override void Update()
    {
        
    }
    private void StartBehavior(GameObject enemy)
    {
        enemy.GetComponent<Animator>().enabled = false;
        enemy.transform.DOMove(Vector3.zero, duration)
            .SetEase(Ease.Flash)
            .OnComplete(() =>
            {
                enemy.GetComponent<Animator>().enabled = true;
                enemy.GetComponent<Enemy>().PlayDustEffect();
                stateManager.groundManager.StopedMove(true);
            });
    }
}
