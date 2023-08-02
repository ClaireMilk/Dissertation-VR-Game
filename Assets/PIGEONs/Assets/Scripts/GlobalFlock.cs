using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlobalFlock : MonoBehaviour {
    public GlobalFlock myFlock;
    //declaring the bird prefab to be instantiated
    public GameObject birdPrefab;
    //declaring the bounding box, inside which the birds will fly around
    public Vector3 flightLimits;

    public void BirdSpeed(float BirdSpeed)
    {
        for (int i = 0; i < numBirds; i++)
        {
            allBirds[i].GetComponent<Flock>().speed = BirdSpeed;
        }
    }
    //declaring the amount of birds to be instantiated
    public int numBirds = 38;
    //declaring the array of all the birds in this flock
    public GameObject[] allBirds;

    public Vector3 goalPos;
    //declaring the condition for the birds to land on their "landing zone" (returnPos), from which the player can "scare" them away
    public bool playerAvoidance;
    //declaring the transform of their "landing zone"
    public Transform returnPos;
    //declaring the AudioSource variable (which will be played whenever the Player enters the "landing zone", thus "scaring" them away
    private AudioSource takeoffAud;

    private bool playerAlert;

    //Draw a red cube, which indicates the flightLimits area
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(flightLimits.x * 2, flightLimits.y * 2, flightLimits.z * 2));
        Gizmos.color = new Color(0, 1, 0, 1);
        Gizmos.DrawSphere(goalPos, 0.1f);
    }

    void Start ()
    {
        myFlock = this;
        goalPos = transform.position;
        allBirds = new GameObject[numBirds];
        goalPos = returnPos.position;
        //if you choose to setup "playerAvoidance", then fetch the AudioSource component (which must be on THIS gameobject)
        if (playerAvoidance)
        {
            takeoffAud = this.GetComponent<AudioSource>();
        }
        //get the amount of birds (from the variable "numBirds" and instantiate all of them inside the "flightLimits" bounding box, as children of this gameobject
        for (int i = 0; i < numBirds; i++)
        {
            Vector3 pos = transform.position + new Vector3
                                    (Random.Range(-flightLimits.x, flightLimits.x),
                                     Random.Range(-flightLimits.y, flightLimits.y),
                                     Random.Range(-flightLimits.z, flightLimits.z));
            allBirds[i] = Instantiate(birdPrefab, pos, Quaternion.identity);
            allBirds[i].transform.parent = gameObject.transform;
        }
    }

    void Update ()
    {
        //if you choose not to setup "playerAvoidance", then the birds should just fly around inside the "flightLimits" bounding box
        if (!playerAvoidance)
        {
            // make the bird's Goal randomly change every 5 seconds, within the Flight Limits
            if (Random.Range(0, 10000) < 50)
            {
                goalPos = transform.position + new Vector3(Random.Range(-flightLimits.x, flightLimits.x),
                                      Random.Range(-flightLimits.y, flightLimits.y),
                                      Random.Range(-flightLimits.z, flightLimits.z));
            }
        }
        if (playerAlert)
        {
            // make the bird's Goal randomly change every 5 seconds, within the Flight Limits
            if (Random.Range(0, 10000) < 50)
            {
                goalPos = transform.position + new Vector3(Random.Range(-flightLimits.x, flightLimits.x),
                                      Random.Range(-flightLimits.y, flightLimits.y),
                                      Random.Range(-flightLimits.z, flightLimits.z));
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (playerAvoidance)
        {
            //when the gameobject tagged "Player" enters the "landig zone" Collider, the birds should continue with their flight
            if (other.gameObject.CompareTag("Player"))
            {
                for (int i = allBirds.Length - 1; i >= 0; i--)
                {
                    allBirds[i].GetComponent<Flock>().enabled = true;

                    Animator[] animators = allBirds[i].GetComponentsInChildren<Animator>();
                    foreach (Animator anim in animators)
                        anim.SetBool("Fly", true);
                }
                playerAlert = true;
                takeoffAud.Play();
            }
            //when a gameobject tagged "Pigeon" enters the "landing zone" Collider, it should stop flying and land
            else if (other.gameObject.tag == "Pigeon")
            {

                Animator[] animators = other.GetComponentsInChildren<Animator>();
                foreach (Animator anim in animators)
                    anim.SetBool("Fly", false);
                
                other.GetComponent<Flock>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (playerAvoidance)
        {
            //when the gameobject tagged "Player" leaves the "landig zone" Collider, the birds should return to their "returnPos" transform
            if (collider.CompareTag("Player"))
            {
                goalPos = returnPos.position;
                playerAlert = false;
            }
        }
    }
}
