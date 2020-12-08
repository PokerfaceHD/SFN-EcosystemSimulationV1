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
    public float secondsUntilHungerReduction = 1;
    void Update()
    {
        if (hunger > 100)
        {
            hunger = 100;
            Instantiate(this, transform.position + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)), new Quaternion(0, 0, 0, 0));
        }
        if(closestDirection.magnitude > 10 && closest != null)
        {
            moveDirection = closestDirection.normalized;
        }
        if (timeTillNextHungerReduction < Time.time)
        {
            timeTillNextHungerReduction = secondsUntilHungerReduction + Time.time;
            hunger--;
        }
        if (timeToNextFoodFind < Time.time)
        {
            timeToNextFoodFind = Time.time + foodFindingCooldown;
            if (hunger > 0)
            {
                if (plant)
                {
                    gos = GameObject.FindGameObjectsWithTag("Plant");
                }
                else
                {
                    gos = GameObject.FindGameObjectsWithTag("Meat");
                }
                closest = null;
                float distance = Mathf.Infinity;
                Vector3 position = transform.position;
                foreach (GameObject go in gos)
                {
                    diff = go.transform.position - position;
                    float curDistance = diff.sqrMagnitude;
                    if (curDistance < distance)
                    {
                        closest = go;
                        distance = curDistance;
                    }
                }
                closestDirection = closest.transform.position - transform.position;
                if ((closest.transform.position - transform.position).magnitude > detectionRange) 
                {
                    Vector3 randomDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
                    moveDirection = randomDirection.normalized;
                    timeToNextFoodFind = Time.time + foodFindingCooldown * 5;
                }
                else
                {
                    moveDirection = closestDirection.normalized;
                }
            }
        }
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (meat)
        {
            if(collision.collider.tag == "Meat")
            {
                hunger += 25 * digestionEfficiencyMeat;
                Destroy(collision.collider.gameObject);
            }
        }
        if (plant)
        {
            if (collision.collider.tag == "Plant")
            {
                hunger += 15 * digestionEfficiencyPlant;
                Destroy(collision.collider.gameObject);

            }
        }
    }
}
