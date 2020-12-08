using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFoodSpawning : MonoBehaviour
{
    float timeTillNextSpawn;
    public float spawnCooldown;
    public GameObject food;
    public Test test;
    float sphereRadius;
    bool plantInRadius;
    Vector3 spawnPoint;
    Vector2 regionSize;
    Collider[] hitColliders;
    public float foodSpawnedAtStart;
    void Start()
    {
        regionSize = test.regionSize;
        sphereRadius = test.radius;
    }

    void Update()
    {
        if (timeTillNextSpawn < Time.time)
        {
            spawnFood();
        }
    }

    void spawnFood()
    {
        plantInRadius = false;
        timeTillNextSpawn = Time.time + spawnCooldown;
        spawnPoint = new Vector3(Random.Range(-regionSize.x / 2, regionSize.y / 2), 0, Random.Range(-regionSize.y / 2, regionSize.y / 2));
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
            Instantiate(food, spawnPoint, new Quaternion(0, 0, 0, 0));
        }
    }
}
