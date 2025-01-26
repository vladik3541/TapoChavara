using System;
using System.Collections;
using UnityEngine;

public class AccumulatedDamage : MonoBehaviour
{
    public event Action ActiveSlider;
    public event Action NoActiveSlider;
    [SerializeField] private float DamageMultiplierThreshold = 10f;
    private float accumulatedClicks = 0;
    private float decreaseRate = 4f; // Кількість зменшення за секунду
    private DamagePerClick observers;
    private GameStateManager stateManager;
    private UIManager uiManager;

    private float timeActive = 4;
    private float time;

    public void Initialize()
    {
        uiManager = FindObjectOfType<UIManager>();
        observers = FindObjectOfType<DamagePerClick>();
        StartCoroutine(DecrementAccumulation());
        uiManager.InitSliderDoubleDamage(DamageMultiplierThreshold);
        stateManager = FindObjectOfType<GameStateManager>();
    }
    private void OnEnable()
    {
        InputManager.OnClick += AccumulateClick;
    }
    private void OnDisable()
    {
        InputManager.OnClick -= AccumulateClick;
    }
    private void AccumulateClick(Vector3 pos)
    {
        if (!stateManager.isGame) return;
        // Збільшуємо накопичене значення кліків
        accumulatedClicks+=1;
        accumulatedClicks = Mathf.Clamp(accumulatedClicks, 0, DamageMultiplierThreshold);
        uiManager.UpdateDoubleDamage(accumulatedClicks);

        ActiveSlider?.Invoke();
        time = timeActive;

        NotifyObservers();
        
       // Debug.Log("Accumulated Clicks: " + accumulatedClicks);
    }
    private IEnumerator DecrementAccumulation()
    {
        while (true)
        {
            // Зменшуємо накопичене значення кліків
            accumulatedClicks = Mathf.Max(0f, accumulatedClicks - decreaseRate * Time.deltaTime);
            uiManager.UpdateDoubleDamage(accumulatedClicks);
            NotifyObservers();
            CheckActive();
            yield return null;
        }
    }
    private void NotifyObservers()
    {
        observers.OnClicksAccumulated(accumulatedClicks);
    }
    private void CheckActive()
    {
        time -= Time.deltaTime;
        if (time <= 0f)
        {
            time = 0f;
            NoActiveSlider?.Invoke();
        }
    }
}
