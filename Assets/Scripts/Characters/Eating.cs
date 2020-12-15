using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : MonoBehaviour
{
    float hunger = 100;
    public bool meat;
    public bool plant;
    GameObject[] gos;
    GameObject target;
    public float foodFindingCooldown = 1;
    float timeToNextFoodFind;
    public float speed = 2;
    public float digestionEfficiencyMeat;
    public float digestionEfficiencyPlant;
    Vector3 moveDirection;
    Vector3 diff;
    GameObject closest;
    Vector3 closestDirection;
    public Rigidbody rb;
    public float detectionRange = 30;
    float timeTillNextHungerReduction;
    public float hungerLossPerSecond = 4;
    RaycastHit hit;
    float moveForSoManyTicks;
    public float addToMoveForSoManyTicks = 50;
    float x;
    float y = 0;
    string typeOfFood;
    void Start()
    {
        if (meat)
        {
            typeOfFood = "Meat";
        }
        else
        {
            typeOfFood = "Plant";
        }
    }
    void Update()
    {
        if (meat)
        {
            if (x < Time.time)
            {
                x += Random.Range(0f, 1f) + Time.time;
                if (Physics.Raycast(transform.position, moveDirection, out hit, 1))
                {
                    if (hit.collider.gameObject.tag == "Plant")
                    {
                        moveForSoManyTicks = addToMoveForSoManyTicks;
                    }
                }
            }
        }
        for (int i = 0; i < moveForSoManyTicks; moveForSoManyTicks--)
        {
            transform.position += new Vector3(-moveDirection.z, 0, moveDirection.x).normalized * speed * Time.deltaTime;
        }
        if (hunger > 100)
        {
            hunger = 75;
            Instantiate(this, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), new Quaternion(0, 0, 0, 0));
        }
        else if (hunger < 0)
        {

            Destroy(this.gameObject);
        }
        hunger -= hungerLossPerSecond * Time.deltaTime;
        Collider[] gameObjectsInDetectionRange = Physics.OverlapSphere(transform.position, detectionRange);
        closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Collider gameObject in gameObjectsInDetectionRange)
        {
            if (gameObject.gameObject.tag == typeOfFood)
            {
                diff = gameObject.gameObject.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = gameObject.gameObject;
                    distance = curDistance;
                }
            }
        }  
        if (closest != null)
        {
            closestDirection = closest.transform.position - transform.position;
            moveDirection = closestDirection.normalized;
            Debug.DrawLine(transform.position, closest.transform.position);
        }
        else if (y < Time.time)
        {
            y += 10;
            Vector3 randomDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
            moveDirection = randomDirection.normalized;
            timeToNextFoodFind = Time.time + foodFindingCooldown * 5;
        }
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (meat)
        {
            if(collision.collider.tag == "Meat")
            {
                hunger += 40 * digestionEfficiencyMeat;
                Destroy(collision.collider.gameObject);
                y = 0;
            }
        }
        if (plant)
        {
            if (collision.collider.tag == "Plant")
            {
                hunger += 15 * digestionEfficiencyPlant;
                Destroy(collision.collider.gameObject);
                y = 0;
            }
        }
    }
}
