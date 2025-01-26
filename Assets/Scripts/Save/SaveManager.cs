using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public const string KEY_GOLD = "Gold";
    public const string KEY_CURRENT_INDEX = "Enemy";
    public const string KEY_CURRENT_HEALTH = "Health";
    public void Initialize()
    {
        instance = this;
    }
    public void SaveGold(float value)
    {
        PlayerPrefs.SetFloat(KEY_GOLD, value);
        PlayerPrefs.Save();
    }
    public float GetSaveGold()
    {
        return PlayerPrefs.GetFloat(KEY_GOLD);
    }

    public void SaveEnemyCurrentIndex(int currentIndex)
    {
        PlayerPrefs.SetInt(KEY_CURRENT_INDEX, currentIndex);
        PlayerPrefs.Save();
    }
    public void SaveEnemyCurrentHealth( float currentHealth)
    {
        PlayerPrefs.SetFloat(KEY_CURRENT_HEALTH, currentHealth);
        PlayerPrefs.Save();
    }
    public int GetCurrentIndex()
    {
        return PlayerPrefs.GetInt(KEY_CURRENT_INDEX);
    }
    public float GetCurrentHealth()
    {
        return PlayerPrefs.GetFloat(KEY_CURRENT_HEALTH);
    }
    public void SaveAbility(List<int> ability)
    {
        for (int i = 0; i < ability.Count; i++) 
        {
            PlayerPrefs.SetInt("Ability" + i, ability[i]);
        }
        PlayerPrefs.SetInt("CountAbility", ability.Count);
        PlayerPrefs.Save();
    }
    public bool GetAbilityList(out List<int> abilityList)
    {
        if(PlayerPrefs.HasKey("CountAbility"))
        {
            int count = PlayerPrefs.GetInt("CountAbility");
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                list.Add(PlayerPrefs.GetInt("Ability" + i));
            }
            abilityList = list;
            return true;
        }
        abilityList = null;
        return false;

    }
    
}
