using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed;

    void Update()
    {
        transform.position += (transform.forward * input.axis.y + transform.right * input.axis.x) * speed * Time.deltaTime;
    }
}
