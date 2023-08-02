using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsMovement : MonoBehaviour {
    //change this is the inspector
    public float speed = 6.0f;
    //declaring a fake gravity to "pull" the player downwards (-y direction)
    public float gravity = -9.8f;
    //declaring the CharacterController component
    private CharacterController _charCont;

	// Use this for initialization
	void Start () {
        //get the CharacterController component (which should be a compoment of the gameobject this script is attached to)
        _charCont = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        //get the "A" and "D" keys from input
        float deltaX = Input.GetAxis("Horizontal") * speed;
        //get the "W" and "S" keys from input
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //creating a Vector3 from the "WASD" inputs
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        //limits the movement speed, so you can't move any quicker that the "speed" variable
        movement = Vector3.ClampMagnitude(movement, speed);
        //apply the fake gravity to the player's movement
        movement.y = gravity;
        //ensures the speed the player moves at does not change based on frame rate
        movement *= Time.deltaTime;

        //moving the player, based on the "movement" directions, via the CharacterController component
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);
    }
}
