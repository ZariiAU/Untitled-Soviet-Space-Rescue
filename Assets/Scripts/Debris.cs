using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    Rigidbody rb;
    public DebrisData debrisData;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Damageable damageable;
        if(collision.collider.gameObject.tag == "Player" && collision.collider.gameObject.TryGetComponent(out damageable))
        {
            if(rb.velocity.magnitude > 1)
            {
                switch (debrisData.debrisType)
                {
                    case DebrisType.Small:
                        // Do nothing
                        Debug.Log("Hit " + collision.gameObject.name + " for " + debrisData.damage);
                        break;

                    case DebrisType.Medium:
                        // Damage Player
                        damageable.Damage(debrisData.damage);
                        Debug.Log("Hit " + collision.gameObject.name + " for " + debrisData.damage);
                        break;

                    case DebrisType.Large:
                        // Damage Player
                        Debug.Log("Hit " + collision.gameObject.name + " for " + debrisData.damage);
                        break;
                }
            }
        }
        else if (collision.collider.gameObject.tag == "Station" && collision.collider.gameObject.TryGetComponent(out damageable))
        {
            switch (debrisData.debrisType)
            {
                case DebrisType.Small:
                    // Do nothing
                    break;

                case DebrisType.Medium:
                    damageable.Damage(debrisData.damage);
                    break;

                case DebrisType.Large:
                    // Game Over
                    break;
            }
        }
    }
}
