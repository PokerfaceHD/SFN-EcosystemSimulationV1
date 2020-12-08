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
    void Update()
    {
        if (hunger > 100)
        {
            hunger = 100;
            Instantiate(this, transform.position + new Vector3(Random.Range(-1,1), 0, Random.Range(-1, 1)), new Quaternion(0,0,0,0));
        }
        if (closestDirection.magnitude > detectionRange)
        {
            moveDirection = (closest.transform.position - transform.position).normalized;
        }
        else
        {
            Vector3 randomDirection = new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f));
            moveDirection = randomDirection.normalized;
        }
        if (timeToNextFoodFind < Time.time)
        {
            timeToNextFoodFind = Time.time + foodFindingCooldown;
            if (hunger > 0)
            {

                if (meat)
                {
                    gos = GameObject.FindGameObjectsWithTag("Meat");
                }
                if (plant)
                {
                    gos = GameObject.FindGameObjectsWithTag("Plant");
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
                    moveDirection = (closest.transform.position - transform.position).normalized;
                }
            }
        }
        rb.velocity = moveDirection * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (meat)
        {
            if(collision.collider.tag == "Meat")
            {
                hunger += 25 * digestionEfficiencyMeat;
                Destroy(closest);
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
