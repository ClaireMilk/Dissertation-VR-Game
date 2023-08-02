using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsCamera : MonoBehaviour {
    //declaring the mouse axes for camera movement
    public enum RotationAxis
    {
        MouseX = 1,
        MouseY = 2
    }
    //initialising those axes
    public RotationAxis axes = RotationAxis.MouseX;
    //declaring the limits of the camera's vertical rotation
    public float minumumVert = -45.0f;
    public float maximumVert = 45.0f;
    //declaring the sensitivity of the camera movement
    public float sensHorizontal = 10.0f;
    public float sensVertical = 10.0f;
    //declaring the verticle angle the player is looking
    private float _rotationX = 0;
	
	// Update is called once per frame
	void Update () {
        //rotate the player based on mouse input on the X and Y axes
		if(axes == RotationAxis.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensHorizontal, 0);
        }
        else if(axes == RotationAxis.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensVertical;
            //clamps the verticle angle between the minimum and maximum limits
            _rotationX = Mathf.Clamp(_rotationX, minumumVert, maximumVert);
            //keeps the Y angle fixed, so there's no horizontal rotation
            float rotationY = transform.localEulerAngles.y;
            //creates a new vector for our stored rotation values
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
	}
}
