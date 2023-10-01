using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebrisSpawner : MonoBehaviour
{
    public static DebrisSpawner instance;

    public int smallDebrisActive;
    public int largeDebrisActive;
    public int mediumDebrisActive;

    [Header("Debris Setup")]
    [SerializeField] GameObject debrisBasePrefab;
    [SerializeField] DebrisData smallData;
    [SerializeField] DebrisData mediumData;
    [SerializeField] DebrisData largeData;
    [SerializeField] GameObject station;

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

    [Header("Spawn Distance Settings")]
    [SerializeField] float maxSmallSpawnDistance = 50;
    [SerializeField] float maxMediumSpawnDistance = 50;
    [SerializeField] float maxLargeSpawnDistance = 50;

    [SerializeField] float minSmallSpawnDistance = -50;
    [SerializeField] float minMediumSpawnDistance = -50;
    [SerializeField] float minLargeSpawnDistance = -50;

    [Header("Spawn Boundary Settings")]
    [SerializeField] float maxBoundary = 50;
    [SerializeField] float minBoundary = -50;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(station.transform.position, minMediumSpawnDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(station.transform.position, maxMediumSpawnDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(station.transform.position, minSmallSpawnDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(station.transform.position, maxSmallSpawnDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(station.transform.position, minLargeSpawnDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(station.transform.position, maxLargeSpawnDistance);
    }

    private void Start()
    {
        PlayerTracker.instance.Player.GetComponent<Inventory>().OnItemPickedUp.AddListener((debris) => 
        { 
            if(debris.debrisData.debrisType == DebrisType.Small)
            {
                smallDebrisActive--;
            }
            else if (debris.debrisData.debrisType == DebrisType.Medium)
            {
                mediumDebrisActive--;
            }
            else if (debris.debrisData.debrisType == DebrisType.Large)
            {
                largeDebrisActive--;
            }
        });
        PlayerTracker.instance.Player.GetComponent<Inventory>().OnItemDropped.AddListener((debris) =>
        {
            if (debris.debrisData.debrisType == DebrisType.Small)
            {
                smallDebrisActive++;
            }
            else if (debris.debrisData.debrisType == DebrisType.Medium)
            {
                mediumDebrisActive++;
            }
            else if (debris.debrisData.debrisType == DebrisType.Large)
            {
                largeDebrisActive++;
            }
        });

        // Spawn only smalls at start
        for (int i = 0; i < maxSmallAmount; i++)
        {
            var debris = SpawnDebris(DebrisType.Small, true);
            smallDebrisActive++;
            debris.name = debris.name + i;
        }
    }

    private void Update()
    {
        if(ScoreManager.instance.Points >= mediumSpawnThreshold && mediumDebrisActive < maxMediumAmount)
        {
            SpawnDebris(DebrisType.Medium, true);
            mediumDebrisActive++;
        }
        else if (ScoreManager.instance.Points >= largeSpawnThreshold && largeDebrisActive < maxLargeAmount)
        {
            SpawnDebris(DebrisType.Large, true);
            largeDebrisActive++;
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
        var b = debris.GetComponent<Debris>();
        b.debrisData = data;

        if (randomiseLocation)
        {
            switch (b.debrisData.debrisType)
            {
                case DebrisType.Small:
                    Vector3 pos = new Vector3(
                        Random.Range(minBoundary, maxBoundary),
                        Random.Range(minBoundary, maxBoundary),
                        Random.Range(minBoundary, maxBoundary));

                    Vector3 temp = pos - station.transform.position;
                    temp = Vector3.Normalize(temp);

                    pos = temp * Random.Range(minSmallSpawnDistance, maxSmallSpawnDistance);
                    debris.transform.position = pos;

                    break;

                case DebrisType.Medium:
                    Vector3 pos0 = new Vector3(
                        Random.Range(minBoundary, maxBoundary),
                        Random.Range(minBoundary, maxBoundary),
                        Random.Range(minBoundary, maxBoundary));

                    Vector3 temp0 = pos0 - station.transform.position;
                    temp = Vector3.Normalize(temp0);

                    pos = temp * Random.Range(minMediumSpawnDistance, maxMediumSpawnDistance);
                    debris.transform.position = pos;
                    break;

                case DebrisType.Large:
                    Vector3 pos1 = new Vector3(
                        Random.Range(minBoundary, maxBoundary),
                        Random.Range(minBoundary, maxBoundary),
                        Random.Range(minBoundary, maxBoundary));

                    Vector3 temp1 = pos1 - station.transform.position;
                    temp = Vector3.Normalize(temp1);

                    pos = temp * Random.Range(minLargeSpawnDistance, maxLargeSpawnDistance);
                    debris.transform.position = pos;
                    break;
            }
        }

        debris.transform.rotation = Quaternion.LookRotation(new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), Random.Range(-50, 50)));

        // Give the object a bit of spin for flavour.
        debris.GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.Range(minRotationSpeed, maxRotationSpeed), Random.Range(minRotationSpeed, maxRotationSpeed), Random.Range(minRotationSpeed, maxRotationSpeed));
        if(b.debrisData.debrisType == DebrisType.Large)
            b.OnDestroyed.AddListener(() => { largeDebrisActive--; });
    }
}