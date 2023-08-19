using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAI : MonoBehaviour
{
    public Transform targetTransform;
    Vector3 moveToPosition;
    Vector3 moveToDirection;
    public float moveSpeed;

    Vector3 directionResult;
    float angleResult;

    public GameObject player;

    Rigidbody dogRigidBody;
    Animator dogAnimator;

    public bool start = false;
    public float startTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        dogRigidBody = GetComponent<Rigidbody>();
        dogAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (start)
        {
            if(dogAnimator.GetBool("Start") != true)
            {
                dogAnimator.SetBool("Start", true);
            }

            if (startTimer >= 1)
            {
                moveToPosition = new Vector3(targetTransform.position.x, gameObject.transform.position.y, targetTransform.position.z);
                moveToDirection = moveToPosition - gameObject.transform.position;

                if ((gameObject.transform.position - moveToPosition).magnitude >= 0.05)
                {
                    dogRigidBody.velocity = moveToDirection.normalized * (moveSpeed + (gameObject.transform.position - moveToPosition).magnitude);
                    dogAnimator.SetBool("Moving", true);

                    directionResult = Vector3.Cross(gameObject.transform.forward, moveToDirection);
                    angleResult = Vector3.Angle(moveToDirection, gameObject.transform.forward);
                }
                else
                {
                    dogRigidBody.velocity = Vector3.zero;
                    dogAnimator.SetBool("Moving", false);

                    Vector3 playerForward = player.transform.forward;
                    directionResult = Vector3.Cross(gameObject.transform.forward, playerForward);
                    angleResult = Vector3.Angle(playerForward, gameObject.transform.forward);
                }


                if (angleResult >= 5)
                {
                    if (directionResult.y > 0)
                    {
                        gameObject.transform.Rotate(0, 200 * Time.deltaTime, 0);
                    }
                    else if (directionResult.y < 0)
                    {
                        gameObject.transform.Rotate(0, -200 * Time.deltaTime, 0);
                    }
                }
            }
            else
            {
                startTimer += Time.deltaTime;
            }
        }
    }
}