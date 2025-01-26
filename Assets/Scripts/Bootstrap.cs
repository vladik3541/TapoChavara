using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameStateManager gameStateManager;
    public UIAnimation uIAnimation;
    [SerializeField] private SpawnEnemy spawnEnemy;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DamagePerClick damagePerClick;
    [SerializeField] private DamagePerSeconds damagePerSeconds;
    [SerializeField] private AccumulatedDamage accumulatedDamage;
    [SerializeField] private GoldManager goldManager;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private ProjectileSpawner projectileSpawner;
    [SerializeField] private EffectClickManager effectClickManager;
    [SerializeField] private AudioManager audioManager;
    

    private void Awake()
    {
        saveManager.Initialize();
        goldManager.Initialize();
        upgradeManager.Initialize();
        uiManager.Initialize(uIAnimation, damagePerClick);
        spawnEnemy.Initialize();
        gameStateManager.Initialize();
        damagePerClick.Initialize(upgradeManager, gameStateManager, uiManager);
        damagePerSeconds.Initialize();
        projectileSpawner.Initialize(damagePerClick, upgradeManager);
        accumulatedDamage.Initialize();
        effectClickManager.Initialize(damagePerClick);
        audioManager.Initialize(damagePerClick);
        
    }

}
