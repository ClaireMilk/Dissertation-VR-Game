using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRope : MonoBehaviour
{
    public DogAI dog;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.transform.parent = other.transform;
            gameObject.transform.position = Vector3.zero;
            dog.start = true;
        }
    }
}
