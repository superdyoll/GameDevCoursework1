using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Rigidbody rb;
    public float minimumSpeed = 0.06f;

	// Use this for initialization
	void Start () {
        rb.AddForce(-1000, 10, 0);
	}
	
	// Update is called once per frame
	void Update () {
        float speed = rb.velocity.magnitude / 5;

        Vector3 velocity = rb.velocity;

        velocity.Normalize();

        print(speed);

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //move right
            rb.AddForce(velocity.z * (speed), 0, velocity.x*(-speed));
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //move left
            rb.AddForce(velocity.z * (-speed), 0, velocity.x*(speed));
        }
        if (Input.GetKey(KeyCode.UpArrow) && speed == 0)
        {

        }
        if (speed < minimumSpeed)
        {
            rb.velocity = Vector3.zero;
        }
    
    }
}
