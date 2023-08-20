using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    public DogAI dog;
    public GameObject hand;
    private void Start()
    {
        gameObject.transform.parent = hand.transform;
        gameObject.transform.position = hand.transform.position;
        dog.start = true;
        gameObject.GetComponent<PlayerRope>().enabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("111");
            gameObject.transform.parent = collision.transform;
            gameObject.transform.position = Vector3.zero;
            dog.start = true;
            gameObject.GetComponent<PlayerRope>().enabled = false;
        }
        else
        {
            Debug.Log("no");
        }
    }
}
