using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public float Health { get; set; }
    public void Damage(float damage);
    public void SetHealth(float amount);
    public void Die();
}
