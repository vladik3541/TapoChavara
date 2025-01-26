using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(UIAnimation))]
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject stateCharacterPanel;
    [SerializeField] private TextMeshProUGUI textCountGold;
    [SerializeField] private TextMeshProUGUI textEnemyHealth;
    [SerializeField] private Slider sliderEnemyHealth;
    [SerializeField] private Slider sliderDoubleDamage;
    [SerializeField] private TextMeshProUGUI LevelInfo;
    [SerializeField] private TextMeshProUGUI textDoubleDamage;

    [SerializeField] private TextMeshProUGUI textDamagePerClick;

    [SerializeField] private Button buttonShop;
    
    private UIAnimation _uiAnimation;
    private AccumulatedDamage _accumulatedDamage;
    private DamagePerClick _damagePerClick;
    private bool activeUpgradePanel = false;
    public void Initialize(UIAnimation uIAnimation, DamagePerClick damagePerClick)
    {
        _uiAnimation = uIAnimation;
        _damagePerClick = damagePerClick;
        UpdateGold(0);
    }
    public void InitSliderEnemyHealth(float value)
    {
        sliderEnemyHealth.maxValue = value;
        UpdateEnemyHealth(value);
    }
    public void InitSliderDoubleDamage(float value)
    {
        sliderDoubleDamage.maxValue = value;
    }
    public void UpdateGold(float value)
    {

        textCountGold.text = Mathf.Ceil(value).ToString("N0");
    }
    public void UpdateEnemyHealth(float value)
    {
        textEnemyHealth.text = Mathf.Ceil(value).ToString("N0");
        sliderEnemyHealth.value = value;

    }
    public void UpdateDoubleDamage(float value)
    {
        sliderDoubleDamage.value = value;
    }
    public void UpdateDamagePerClick(float value)
    {
        textDamagePerClick.text = "Damage per click" + value.ToString("N0") + "+";
    }
    public void SwitchUpgradePanel()
    {
        activeUpgradePanel = !activeUpgradePanel;

        if (activeUpgradePanel)
            buttonShop.transform.Rotate(0, 0, 180);
        else
            buttonShop.transform.Rotate(0, 0, 180);
        if (activeUpgradePanel)
            _uiAnimation.OpenPanelAbility(upgradePanel.GetComponent<RectTransform>());
        else
            _uiAnimation.ClosePanelAbility(upgradePanel.GetComponent<RectTransform>());
    }
    public void UpdateLevel(int level)
    {
        LevelInfo.text = "Level " + level.ToString("F0");
    }
}
