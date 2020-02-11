using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public GlobalFlock myManager;
    public float speed = 0.1f;
    // decides how fast the fish will turn
    float rotationSpeed = 4.0f;
    public float minSpeed = 0.8f;
    public float maxSpeed = 2.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neighbourDistance = 3.0f;
    public Vector3 newGoalPos;
    bool turning = false;
    public float speedMult = 1;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        //this.GetComponent<Animation>()["motion"].speed = speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!turning)
        {
            newGoalPos = this.transform.position - other.gameObject.transform.position;
        }
        turning = true;
    }
    private void OnTriggerExit(Collider other)
    {
        turning = false;
    }
    // Update is called once per frame
    void Update()
    {
        //if reach the edge of tank
        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotationSpeed * Time.deltaTime);
            speed = Random.Range(minSpeed, maxSpeed);
            //this.GetComponent<Animation>()["Motion"].speed = speed;
        }
        else
        {
            if (Random.Range(0, 10) < 1)
            {
                ApplyRules();
            }
        }
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
    void ApplyRules()
    {
        GameObject[] gos;
        gos = GlobalFlock.allFish;
        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPosition = GlobalFlock.goalPosition;
        float distance;
        int groupSize = 0;
        foreach (GameObject gmo in gos)
        {
            //every fish needs to know the position of other fishes and their distances
            if(gmo != this.gameObject)
            {
                distance = Vector3.Distance(gmo.transform.position, this.transform.position);
                if(distance<= neighbourDistance)
                {
                    vcentre += gmo.transform.position;
                    groupSize++;

                    //avoid of being in a small distance
                    if(distance < 0.3f)
                    {
                        vavoid = vavoid + (this.transform.position - gmo.transform.position);
                    }
                    Flock anotherFlock = gmo.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPosition - this.transform.position);
            speed = gSpeed / groupSize * speedMult;
            //this.GetComponent<Animation>()["Motion"].speed = speed;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotationSpeed * Time.deltaTime);
            }
        }
    }
}
