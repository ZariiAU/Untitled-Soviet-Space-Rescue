using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    Rigidbody rb;
    public DebrisData debrisData;

    private void OnCollisionEnter(Collision collision)
    {
        Damageable damageable;
        if(collision.gameObject.tag == "Player" && collision.gameObject.TryGetComponent(out damageable))
        {
            if(rb.velocity.magnitude > 1)
            {
                switch (debrisData.debrisType)
                {
                    case DebrisType.Small:
                        // Do nothing
                        break;

                    case DebrisType.Medium:
                        // Damage Player
                        damageable.Damage(debrisData.damage);
                        Debug.Log("Hit " + collision.gameObject.name + " for " + debrisData.damage);
                        break;

                    case DebrisType.Large:
                        // Damage Player
                        break;
                }
            }
        }
        else if (collision.gameObject.tag == "Station" && collision.gameObject.TryGetComponent(out damageable))
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
