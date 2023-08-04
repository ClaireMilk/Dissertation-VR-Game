using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI : MonoBehaviour
{
    public Transform targetTransform;
    Vector3 moveToPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveToPosition = new Vector3(targetTransform.position.x, gameObject.transform.position.y, targetTransform.position.z);
        //Debug.Log("Dog: " + gameObject.transform.position);
        //Debug.Log("Target: " + moveToPosition);
        if (gameObject.transform.position != moveToPosition)
        {
            gameObject.transform.position = moveToPosition;
        }
    }
}