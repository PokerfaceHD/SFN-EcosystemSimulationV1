using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFoodSpawning : MonoBehaviour
{
    float timeTillNextSpawn;
    public float spawnCooldown;
    public GameObject food;
    public GameObject MeatEater;
    public GameObject PlantEater;
    public Test test;
    float sphereRadius;
    bool plantInRadius;
    Vector3 spawnPoint;
    Vector2 regionSize;
    Collider[] hitColliders;
    public float numberOfPlantEatersSpawnedAtStartup = 10;
    public float numberOfMeatEatersSpawnedAtStartup = 2;
    void Start()
    {
        regionSize = test.regionSize;
        sphereRadius = test.radius;

        for (int i = 0; i < numberOfPlantEatersSpawnedAtStartup; i++)
        {
            Instantiate(PlantEater, spawnpointgen(2), new Quaternion(0, 0, 0, 0));
        }

        for (int i = 0; i < numberOfMeatEatersSpawnedAtStartup; i++)
        {
            Instantiate(MeatEater, spawnpointgen(2), new Quaternion(0, 0, 0, 0));
        }
    }

    void Update()
    {
        if (timeTillNextSpawn < Time.time)
        {
            spawnFood();
            timeTillNextSpawn = Time.time + spawnCooldown;
        }
    }

    void spawnFood()
    {
        plantInRadius = false;
        hitColliders = Physics.OverlapSphere(spawnPoint, sphereRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.tag == "Plant")
            {
                plantInRadius = true;
                break;
            }
        }
        if (!plantInRadius)
        {
            Instantiate(food, spawnpointgen(0), new Quaternion(0, 0, 0, 0));
        }
    }

    Vector3 spawnpointgen(float height)
    {
        Vector3 point = new Vector3(Random.Range(-regionSize.x / 2, regionSize.y / 2), height, Random.Range(-regionSize.y / 2, regionSize.y / 2));
        return point;
    }
}
