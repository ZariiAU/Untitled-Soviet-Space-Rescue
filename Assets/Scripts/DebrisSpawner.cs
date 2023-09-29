using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    [Header("Debris Setup")]
    [SerializeField] GameObject debrisBasePrefab;
    [SerializeField] DebrisData smallData;
    [SerializeField] DebrisData mediumData;
    [SerializeField] DebrisData largeData;

    [Header("Spawn Amount")]
    [SerializeField] uint maxSmallAmount;
    [SerializeField] uint maxMediumAmount;
    [SerializeField] uint maxLargeAmount;

    [Header("Spawn Conditions")]
    [SerializeField] uint mediumSpawnThreshold;
    [SerializeField] uint largeSpawnThreshold;

    [SerializeField] uint mediumSpawnChance;
    [SerializeField] uint largeSpawnChance;

    [Header("Idle Rotation Speeds")]
    [SerializeField] float maxRotationSpeed = 0.5f;
    [SerializeField] float minRotationSpeed = 0.01f;

    [Header("Spawn Boundary Settings")]
    [SerializeField] float maxSpawnDistance = 50;
    [SerializeField] float minSpawnDistance = -50;

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
                InitialiseDebris(debrisInstance, smallData);
                break;
            case DebrisType.Medium:
                InitialiseDebris(debrisInstance, mediumData);
                debrisInstance.transform.localScale = debrisInstance.transform.localScale * 3;
                break;
            case DebrisType.Large:
                InitialiseDebris(debrisInstance, largeData);
                debrisInstance.transform.localScale = debrisInstance.transform.localScale * 10;
                break;
        }
    }

    void InitialiseDebris(GameObject debris, DebrisData data)
    {
        debris.GetComponent<Debris>().debrisData = data;
        debris.transform.position = new Vector3(Random.Range(minSpawnDistance, maxSpawnDistance), Random.Range(minSpawnDistance, maxSpawnDistance), Random.Range(minSpawnDistance, maxSpawnDistance));
        debris.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));
        debris.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(minRotationSpeed, maxRotationSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), Random.Range(minRotationSpeed, maxRotationSpeed));
    }
}