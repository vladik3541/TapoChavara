using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static event Action onUpgrade;
    public event Action<Ability> onUpgradeComplete;
    
    [SerializeField] private List<Ability> upgradesList; //All the abilities that are

    private List<int> countUpgradeLevel; // How many times has this ability been acquired?
    private int totalDamage;

    public List<Ability> UpgradesList { get { return upgradesList; } }
    public List<int> CountUpgradeLevel { get { return countUpgradeLevel; } }

    public void Initialize()
    {
        //find all buttons with scripts AbilittiCell
        AbilittiCell[] abilittiCells = FindObjectsOfType<AbilittiCell>();
        foreach (var item in abilittiCells)
        {
            item.Inirialize(this);
        }
        
        //Load data how buy abillity
        countUpgradeLevel = new List<int>();

        if(SaveManager.instance.GetAbilityList(out List<int> _list))
        {
            countUpgradeLevel = _list;
            foreach (AbilittiCell cell in abilittiCells)
            {
                if (cell != null)
                {
                    cell.UpdateTextUpgrade();
                }
                else
                {
                    Debug.LogError("One of the abilittiCells is null!");
                }
            }
            Debug.Log("Success");
        }
        else
        {
            // Ініціалізація рівнів апгрейдів
            for (int i = 0; i < upgradesList.Count; i++)
            {
                countUpgradeLevel.Add(0);
            }
        }
    }
    public void UpgradeElement(Ability ability)
    {
        if (GoldManager.instance.RemoveGold(ability.Price))
        {
            for (int i = 0; i < countUpgradeLevel.Count; i++)
            {
                if(ability.name == upgradesList[i].name)
                {
                    countUpgradeLevel[i]++;
                }
            }
            if(ability.Projectile != null)
                onUpgradeComplete?.Invoke(ability);

            SaveManager.instance.SaveAbility(countUpgradeLevel);
            onUpgrade?.Invoke();
            Debug.Log($"{ability.Name} upgraded to level");
        }
        else
        {
            Debug.LogWarning($"Upgrade {ability.Name} does not exist in the dictionary.");
        }
    }
    
    public int GetAllDamagePerClick()
    {
        totalDamage = 0;
        foreach (Ability ability in upgradesList)
        {
            if(!ability.IsDamagePerSeconde)
                totalDamage += GetUpgradeLevel(ability) * ability.Damage;
        }
        return totalDamage;
    }
    public int GetAllDamagePerSeconds()
    {
        totalDamage = 0;
        foreach (Ability ability in upgradesList)
        {
            if (ability.IsDamagePerSeconde)
                totalDamage += GetUpgradeLevel(ability) * ability.Damage;
        }
        return totalDamage;
    }
    public int GetUpgradeLevel(Ability ability)
    {
        for (int i = 0; i < upgradesList.Count; i++)
        {
            if (ability.name == upgradesList[i].name)
            {
                return countUpgradeLevel[i];
            }
        }
        Debug.LogWarning($"Upgrade {ability.Name} does not exist in the dictionary.");
        return 0;
    }
}
