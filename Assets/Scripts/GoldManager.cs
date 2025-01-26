using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    [SerializeField]private float gold;

    public float Gold
    {
        get
        {
            return gold;
        }
    }
    private UIManager manager;
    public void Initialize()
    {
        instance = this;
        manager = FindObjectOfType<UIManager>();
        if(PlayerPrefs.HasKey(SaveManager.KEY_GOLD))
            gold = SaveManager.instance.GetSaveGold();
    }

    public bool RemoveGold(float value)
    {
        if(gold >= value)
        {
            gold-=value;

            manager.UpdateGold(gold);
            SetSaveGold();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void AddGold(float value)
    {
        gold += value;
        manager.UpdateGold(gold);
        SetSaveGold();
        
    }
    private void SetSaveGold()
    {
        SaveManager.instance.SaveGold(Gold);
    }
}
