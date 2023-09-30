using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    public static DebrisSpawner instance;

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

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < maxSmallAmount; i++)
        {
            var debris = SpawnDebris(DebrisType.Small, true);
            debris.name = debris.name + i;
        }
        for (int i = 0; i < maxMediumAmount; i++)
        {
            var debris = SpawnDebris(DebrisType.Medium, true);
            debris.name = debris.name + i;
        }
        for (int i = 0; i < maxLargeAmount; i++)
        {
            var debris = SpawnDebris(DebrisType.Large, true);
            debris.name = debris.name + i;
        }
    }

    public GameObject SpawnDebris(DebrisType debrisType, bool randomiseLocation)
    {
        GameObject debrisInstance = Instantiate(debrisBasePrefab);

        switch (debrisType)
        {
            case DebrisType.Small:
                InitialiseDebris(debrisInstance, smallData, randomiseLocation);
                break;
            case DebrisType.Medium:
                InitialiseDebris(debrisInstance, mediumData, randomiseLocation);
                debrisInstance.transform.localScale = debrisInstance.transform.localScale * 3;
                break;
            case DebrisType.Large:
                InitialiseDebris(debrisInstance, largeData, randomiseLocation);
                debrisInstance.transform.localScale = debrisInstance.transform.localScale * 10;
                break;
        }
        return debrisInstance;
    }

    void InitialiseDebris(GameObject debris, DebrisData data, bool randomiseLocation)
    {
        var b = debris.GetComponent<Debris>().debrisData = data;

        if (randomiseLocation)
            debris.transform.position = new Vector3(Random.Range(minSpawnDistance, maxSpawnDistance), Random.Range(minSpawnDistance, maxSpawnDistance), Random.Range(minSpawnDistance, maxSpawnDistance));


        debris.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

        // Give the object a bit of spin for flavour.
        debris.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(minRotationSpeed, maxRotationSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), Random.Range(minRotationSpeed, maxRotationSpeed));
    }
}