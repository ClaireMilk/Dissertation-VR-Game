using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    //declaring the global flock script variable
    public GlobalFlock myFlockManager;
    readonly float rotationSpeed = 6.0f;
    //declaring the interval from which the "speed" variable is calculated
    public float minSpeed; // 1 recommended
    public float maxSpeed; // 2 recommended
    public float speed;
    //adjust this float value to set the distance limit between any two birds flying together; call it neighbour distance limit
    public float neighbourDistanceLimit;  

    Vector3 averageHeading;
    Vector3 averagePosition;

    //adjust this HERE to set the distance between each bird
    //readonly float neighbourDistance = 5.0f;

    public bool turning = false;

    //declaring a bool condition, to determinte if your bird is going to use a random Material, or not
    [Header("Tick this, and assign Materials")]
    public bool randomMaterial;
    //declaring the SkinnedMeshRenderer of this bird (for which to set it's material) & a list of Materials to select from, if you wish to give each bird a random Material
    private SkinnedMeshRenderer mySkin;
    public Material[] allSkinMats;

	void Start ()
    {
        //calculating a random float between the min and max speed
        speed = Random.Range(minSpeed, maxSpeed);
        //fetching the global flock script variable
        myFlockManager = gameObject.GetComponentInParent<GlobalFlock>();
        //if you ticked the bool "randomMaterial" in the inspector
        if (randomMaterial)
        {
            //fetching the Material of this bird
            mySkin = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            //assigning a random material from the list of Materials
            mySkin.material = allSkinMats[Random.Range(0, allSkinMats.Length)];
        }
    }
    
    void Update ()
    {
        //determine the bounding box of the manager cube
        Bounds b = new Bounds(myFlockManager.transform.position, myFlockManager.flightLimits * 2);

        //if bird is outside the bounds of the cube then start turning around
        if (!b.Contains(transform.position))
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if (turning)
        {
            //turn towards the centre of the manager cube
            Vector3 direction = myFlockManager.transform.position - transform.position;
            //Vector3 direction = newGoalPos - transform.position;    // tried this for obstacle avoidance
            transform.rotation = Quaternion.Slerp(transform.rotation,
                                                    Quaternion.LookRotation(direction),
                                                    rotationSpeed * Time.deltaTime);
            speed = Random.Range(minSpeed, maxSpeed);
        }
        else
        {
            if (Random.Range(0,10) < 1)
                ApplyRules();
        }
        //making the bird fly around
        transform.Translate(0, 0, Time.deltaTime * speed);
    }
    //the rules, which make the birds fly in a flock
    void ApplyRules()
    {
        GameObject[] gos;
        gos = myFlockManager.allBirds;

        Vector3 vcentre = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.2f;

        Vector3 goalPos = myFlockManager.goalPos;

        float dist;

        int groupSize = 0;
        foreach (GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                //calculate the neighbour distance
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= neighbourDistanceLimit)
                {
                    vcentre += go.transform.position;
                    groupSize++;

                    //adjust this float value to set the distance limit between any two birds flying together; call it neighbour distance limit
                    if(dist < neighbourDistanceLimit)
                    {
                        vavoid = vavoid + (this.transform.position - go.transform.position);
                    }
                    //adjusting the bird's speed to match the average speed of the entire flock
                    Flock anotherFlock = go.GetComponent<Flock>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        //if there are more the 0 birds in the flock, calculate their average speed, and their general direction of flight
        if(groupSize > 0)
        {
            vcentre = vcentre / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vcentre + vavoid) - transform.position;
            if (direction != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation,
                                                      Quaternion.LookRotation(direction),
                                                      rotationSpeed * Time.deltaTime);
        }
    }
}
