using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeath;
    public UnityEvent OnDamaged;

    public float Health { get; set; }

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
