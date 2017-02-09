using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb.AddForce(-1000, 10, 0);
	}
	
	// Update is called once per frame
	void Update () {
        float speed = rb.velocity.magnitude / 5;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //move right
            rb.AddForce(0, 0, speed);
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //move left
            rb.AddForce(0, 0, -speed);
        }
    
    }
}
