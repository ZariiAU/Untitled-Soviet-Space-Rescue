using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Explosive : MonoBehaviour
{
    [SerializeField] float timer;
    float elapsedTime;
    public UnityEvent OnExplode;
    public GameObject attachedObject;
    DebrisSpawner ds;

    private void Start()
    {
        ds = DebrisSpawner.instance;
        attachedObject.GetComponent<Damageable>().OnDeath.AddListener(() => { 
            ds.SpawnDebris(DebrisType.Medium, false).transform.position = attachedObject.transform.position;
            ds.SpawnDebris(DebrisType.Medium, false).transform.position = attachedObject.transform.position;
            ds.SpawnDebris(DebrisType.Medium, false).transform.position = attachedObject.transform.position;
            ds.SpawnDebris(DebrisType.Medium, false).transform.position = attachedObject.transform.position;
            Destroy(attachedObject); });
    }

    private void Update()
    {
        if (elapsedTime < timer)
        {
            elapsedTime += Time.deltaTime;
        }
        else if (elapsedTime >= timer)
        {
            OnExplode.Invoke();
            
            Debug.Log("boom");

            elapsedTime = 0;
            attachedObject.GetComponent<Damageable>().Damage(1);
            attachedObject.GetComponent<Debris>().OnDestroyed.Invoke();
            Destroy(this);
        }
    }
}
