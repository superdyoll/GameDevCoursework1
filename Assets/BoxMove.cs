using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour {

    public GameObject cube;
    private Rigidbody rb;


    public float smooth = 1;

    // Use this for initialization
    void Start () {
		rb = cube.GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        edsMovement();
    }

    private void edsMovement()
    {
        //vert is w/s
        //horiz is a/d

        Vector3 oldPostion = transform.position;
        Vector3 velocity = rb.velocity;
        Ray ray;
        int layerMask = (1 << 8);
        RaycastHit hitInfo;
        //check if can move in total or partial
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")));
        ray = new Ray(transform.position, Vector3.down);
        if (!Physics.Raycast(ray, out hitInfo, 100f, layerMask))
        {
            transform.position = oldPostion;
            //check and move Horizontally
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f));
            ray = new Ray(transform.position, Vector3.down);
            if (!Physics.Raycast(ray, out hitInfo, 100f, layerMask))
            {
                transform.position = oldPostion;
            }
            else
            {
                oldPostion = transform.position;
            }
            //check and move Vertically
            transform.Translate(new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical")));

            ray = new Ray(transform.position, Vector3.down);
            if (!Physics.Raycast(ray, out hitInfo, 100f, layerMask))
            {
                transform.position = oldPostion;
            }
            else
            {
                oldPostion = transform.position;
            }
        }

        ray = new Ray(transform.position, Vector3.down);

        if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
        {
            //Quaternion newRotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")), hitInfo.transform.forward);
            //newRotation *= Quaternion.Euler(0, 90, 0);
            Quaternion temp = Quaternion.LookRotation(hitInfo.transform.right, hitInfo.transform.up);
            transform.rotation = temp;
        }
    }

    //take vector input for direction, current velocity and returns a new velocity as a vecotr3
    Vector3 newtonianMotion(Vector3 direction, Vector3 currentVelocity)
    {
        float maxVelocity = 10;
        float acceleration = 0.5f;

        Vector3 newVelocity = currentVelocity + (acceleration * direction) * Time.deltaTime;

        if (newVelocity.x > maxVelocity)
        {
            newVelocity.x = maxVelocity;
        }
        if (newVelocity.z > maxVelocity)
        {
            newVelocity.z = maxVelocity;
        }

        return newVelocity;
    }


}
