using System;
using System.Collections;
using UnityEngine;

public class DamagePerClick : MonoBehaviour
{
    public event Action<Vector3> OnClickHit;
    public event Action OnActiveDoubleDamage, OnDiactiveDoubleDamage;
    [SerializeField] private float damageMultiplierThreshold;
    [SerializeField] private GameObject textDoubleDamage;

    private UpgradeManager _upgradeManager;
    private GameStateManager _gameStateManager;
    private UIManager _uiManager;


    private float damageMultiplier = 1;
    [SerializeField]private float defaultClicksPerAction = 1;
    public void Initialize(UpgradeManager upgradeManager, GameStateManager gameStateManager, UIManager uIManager)
    {
        _upgradeManager = upgradeManager;
        _gameStateManager = gameStateManager;
        _uiManager = uIManager;
        _uiManager.UpdateDamagePerClick(defaultClicksPerAction);
        textDoubleDamage.SetActive(false);
        RewardedAdsButton.OnCompleteAdsWatch += Active_Double_Damage;
    }
    private void OnEnable()
    {
        InputManager.OnClick += Click;
        
    }
    private void OnDisable()
    {
        InputManager.OnClick -= Click;
        RewardedAdsButton.OnCompleteAdsWatch -= Active_Double_Damage;
    }
   public void OnClicksAccumulated(float accumulatedClicks)
    {
        if (accumulatedClicks >= damageMultiplierThreshold)
        {
            OnActiveDoubleDamage?.Invoke();
            damageMultiplier = 2;
        }
        else
        {
            OnDiactiveDoubleDamage?.Invoke();
            damageMultiplier = 1;
        }
    }
    private void Click(Vector3 pos)
    {
        if (!_gameStateManager.isGame) return;
        _uiManager.UpdateDamagePerClick(GetDamage());
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                float damage = GetDamage();
                OnClickHit?.Invoke(hit.point);

                damageable.TakeDamage(damage);

                GoldManager.instance.AddGold(damage);

            }
        }
    }    
    private void Active_Double_Damage()
    {
        StartCoroutine(GoDoubleDamage());
    }
    IEnumerator GoDoubleDamage()
    {
        damageMultiplier = 2;
        textDoubleDamage.SetActive(true);
        yield return new WaitForSeconds(10);
        textDoubleDamage.SetActive(false);
        damageMultiplier = 1;
    }
    public float GetDamage()
    {
        return (_upgradeManager.GetAllDamagePerClick() + defaultClicksPerAction) * damageMultiplier;
    }
}
