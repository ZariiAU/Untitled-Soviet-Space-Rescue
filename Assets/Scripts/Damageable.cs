using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;

    public float health;

    public void Damage(float damage)
    {
        health -= damage;
        OnDamaged?.Invoke();
        if(health <= 0)
            Die();
    }

    public void Die()
    {
        OnDeath.Invoke();
    }

    public void SetHealth(float amount)
    {
        health = amount;
    }

    public float GetHealth()
    {
        return health;
    }
}
