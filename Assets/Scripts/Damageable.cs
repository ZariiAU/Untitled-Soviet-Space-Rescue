using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;

    [SerializeField] public float Health { get; set; }

    public void Damage(float damage)
    {
        Health -= damage;
        OnDamaged?.Invoke();
    }

    public void Die()
    {
        OnDeath?.Invoke();
    }

    public void SetHealth(float amount)
    {
        Health = amount;
    }
}
