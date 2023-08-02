using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedRandom : MonoBehaviour {
    public float speed;
    public float minSpeed;
    public float maxSpeed;
    private Animator myanim;

    void Start () {
        myanim = this.GetComponent<Animator>();
        speed = Random.Range(minSpeed, maxSpeed);
        myanim.speed = speed;
    }
	
	void Update () {
		
	}
}
