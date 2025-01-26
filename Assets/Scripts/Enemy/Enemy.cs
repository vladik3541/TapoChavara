using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action OnRun;
    [SerializeField] private ParticleSystem dustEffect;

    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void StartMove()
    {
        OnRun?.Invoke();
    }
    private void Rage()
    {
        audioSource.Play();
    }
    public void PlayDustEffect()
    {
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
    }

}
