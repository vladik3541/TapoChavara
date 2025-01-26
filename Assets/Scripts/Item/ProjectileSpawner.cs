using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] LeftPoint,RightPoint;
    [SerializeField] private int countInitializePrefabs = 10;
    private List<PoolObject> projectilePools = new List<PoolObject>();
    private UpgradeManager _upgradeManager;
    private DamagePerClick _damagePerClick;

    public void Initialize(DamagePerClick damagePerClick, UpgradeManager upgradeManager)
    {
        _damagePerClick = damagePerClick;
        _upgradeManager = upgradeManager;

        _damagePerClick.OnClickHit += Spawn;
        _upgradeManager.onUpgradeComplete += UpdateCurrentProjectiles;

        //If the ability has at least one upgrade, it is added.
        for (int i = 0; i < _upgradeManager.UpgradesList.Count; i++)
        {
            if (_upgradeManager.CountUpgradeLevel[i] != 0 && _upgradeManager.UpgradesList[i].Projectile != null)
            {
                projectilePools.Add(new PoolObject(_upgradeManager.UpgradesList[i].Projectile, countInitializePrefabs, _upgradeManager.UpgradesList[i].Name));
            }
        }
    }
    public void UpdateCurrentProjectiles(Ability ability)
    {
        if (projectilePools.Any(pool => pool.Name == ability.Name))
        {
            return; // якщо пул з такою назвою вже ≥снуЇ, н≥чого не робимо.
        }

        // ƒодаЇмо новий пул, оск≥льки його ще немаЇ.
        projectilePools.Add(new PoolObject(ability.Projectile, countInitializePrefabs, ability.Name));
    }
    private void OnDisable()
    {
        _damagePerClick.OnClickHit -= Spawn;
        _upgradeManager.onUpgradeComplete -= UpdateCurrentProjectiles;
    }
    private Vector3 GetRandomPosition()
    {
        // 0 Top
        // 1 Bot
        Vector3 randomPos;
        int randomSide = Random.Range(0, 2);
        if (randomSide == 0)
        {
            randomPos.y = Random.Range(LeftPoint[0].position.y, LeftPoint[1].position.y);
            randomPos.x = LeftPoint[0].position.x;
            randomPos.z = LeftPoint[0].position.z;
            return randomPos;
        }
        else
        {
            randomPos.y = Random.Range(RightPoint[0].position.y, RightPoint[1].position.y);
            randomPos.x = RightPoint[0].position.x;
            randomPos.z = RightPoint[0].position.z;
            return randomPos;
        }

    }
    private void Spawn(Vector3 target)
    {
        if (!projectilePools.Any()) return;

        GameObject project = projectilePools[Random.Range(0, projectilePools.Count)].GetObjectFromPool();

        project.transform.position = GetRandomPosition();
        project.GetComponent<ProjectileController>().StartMovement(target, () => { project.SetActive(false); });
    }

}
