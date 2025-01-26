using System;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public event Action OnEndAllEnemy;
    public event Action<int> OnChangeEnemy;
    [SerializeField] private GameObject[] enemys;
    private int enemyLevel = 0;
    public int EnemyLevel
    {
        get { return enemyLevel; }
    }
    private GameObject currenEnemy;
    public GameObject CurrentEnemy
    {
        get
        {
            return currenEnemy;
        }
    }

    public void Initialize()
    {
        //if (PlayerPrefs.HasKey(SaveManager.KEY_CURRENT_HEALTH))
            //health = SaveManager.instance.GetCurrentHealth();
        if (PlayerPrefs.HasKey(SaveManager.KEY_CURRENT_INDEX))
        {
            enemyLevel = SaveManager.instance.GetCurrentIndex();
            //Debug.LogError($"SaveIndexEnemy it {levelEnemy}");
        }
            
    }
    public GameObject Spawn()
    {
        if (enemyLevel >= enemys.Length)
        {
            OnEndAllEnemy?.Invoke();
            return null;
        }
        currenEnemy = Instantiate(enemys[enemyLevel],new Vector3(10, 20,0), Quaternion.identity);
        OnChangeEnemy?.Invoke(enemyLevel);
        SaveManager.instance.SaveEnemyCurrentIndex(enemyLevel);
        enemyLevel++;
        return currenEnemy;
    }
}
