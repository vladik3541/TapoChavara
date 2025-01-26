using System.Collections;
using UnityEngine;

public class DamagePerSeconds : MonoBehaviour
{
    private int damage;
    private const float TIME_DAMAGE = .1f;

    private SpawnEnemy enemyManager;
    private UpgradeManager upgradeManager;
    private GameStateManager gameStateManager;

    public void Initialize()
    {
        enemyManager = FindObjectOfType<SpawnEnemy>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        gameStateManager = FindObjectOfType<GameStateManager>();
        StartCoroutine(AutoDamage());
    }

    // Update is called once per frame
    IEnumerator AutoDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(TIME_DAMAGE);
            ApllyDamage();
        }
    }
    void ApllyDamage()
    {
        if (enemyManager != null && gameStateManager.isGame)
        {
            damage = upgradeManager.GetAllDamagePerSeconds();

            if(enemyManager.CurrentEnemy != null)
            {
                enemyManager.CurrentEnemy.GetComponent<EnemyHealth>().TakeDamage(damage * TIME_DAMAGE);
            }
            
            GoldManager.instance.AddGold(damage*TIME_DAMAGE);

            //Debug.Log(damage * TIME_DAMAGE);
        }
    }
    
}
