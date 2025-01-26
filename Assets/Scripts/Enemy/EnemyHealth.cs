using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public static event Action OnDeath;
    public float health = 100;
    private float maxHealth;
    private Animator animator;
    private UIManager manager;
    private bool isDie = false;
    [SerializeField] private float regenerationPercent = 1.5f;
    private float regeneration;
    private void Start()
    {
        maxHealth = health;
        manager = FindObjectOfType<UIManager>();
        manager.InitSliderEnemyHealth(health);
        animator = GetComponent<Animator>();
        regeneration = (health / 100) * regenerationPercent;

    }
    public void TakeDamage(float amount)
    {
        if(health>0)
        {
            health -= amount;
            if(health < 0)
                health = 0;
            SaveManager.instance.SaveEnemyCurrentHealth(health);
            manager.UpdateEnemyHealth(health);
        }
        else if (health <= 0)
        {
            if (isDie) return;
            health = 0;
            isDie = true;
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        animator.SetTrigger("Death");
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        OnDeath?.Invoke();
        Destroy(gameObject,5);
    }

    private void Update()
    {
        Regeneration();
    }

    private void Regeneration()
    {
        if (maxHealth > health && health > 0)
        {
            health += Time.deltaTime * regeneration;
        }
    }
}
