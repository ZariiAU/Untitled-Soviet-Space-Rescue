using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [SerializeField] GameObject debrisBasePrefab;
    [SerializeField] DebrisData smallData;
    [SerializeField] DebrisData mediumData;
    [SerializeField] DebrisData largeData;

    [SerializeField] uint maxSmallAmount;
    [SerializeField] uint maxMediumAmount;
    [SerializeField] uint maxLargeAmount;

    [SerializeField] uint mediumSpawnThreshold;
    [SerializeField] uint largeSpawnThreshold;

    [SerializeField] uint mediumSpawnChance;
    [SerializeField] uint largeSpawnChance;

    private void Start()
    {
        for (int i = 0; i < maxSmallAmount; i++)
        {
            SpawnDebris(DebrisType.Small);
        }
        for (int i = 0; i < maxMediumAmount; i++)
        {
            SpawnDebris(DebrisType.Medium);
        }
        for (int i = 0; i < maxLargeAmount; i++)
        {
            SpawnDebris(DebrisType.Large);
        }
    }

    void SpawnDebris(DebrisType debrisType)
    {
        GameObject debrisInstance = Instantiate(debrisBasePrefab);

        switch (debrisType)
        {
            case DebrisType.Small:
                debrisInstance.GetComponent<Debris>().debrisData = smallData;
                debrisInstance.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
                debrisInstance.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));
                break;
            case DebrisType.Medium:
                debrisInstance.GetComponent<Debris>().debrisData = mediumData;
                debrisInstance.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
                debrisInstance.transform.localScale = debrisInstance.transform.localScale * 3;
                debrisInstance.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));
                break;
            case DebrisType.Large:
                debrisInstance.GetComponent<Debris>().debrisData = largeData;
                debrisInstance.transform.position = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50));
                debrisInstance.transform.localScale = debrisInstance.transform.localScale * 10;
                debrisInstance.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));
                break;
        }
    }
}