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
        Vector3 velocity = rb.velocity;
        velocity.Normalize();
        velocity = new Vector3(velocity.x, 0, velocity.z);
        rb.transform.rotation = Quaternion.LookRotation(velocity, Vector3.up); //Make the object always face the direction it is moving in

        //vert is w/s
        //horiz is a/d

        Vector3 oldPostion = transform.position;

        Transform potentialTransform = transform;
        
        Ray ray;
        int layerMask = (1 << 8);
        RaycastHit hitInfo;
        //check if can move in total or partial
        potentialTransform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")));
        ray = new Ray(potentialTransform.position, Vector3.down);

        if (!Physics.Raycast(ray, out hitInfo, 100f, layerMask))
        {
            potentialTransform.position = oldPostion;
            //check and move Horizontally
            potentialTransform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f));
            ray = new Ray(potentialTransform.position, Vector3.down);

            if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
            {
                oldPostion = potentialTransform.position;
            }
            else
            {
                potentialTransform.position = oldPostion;
            }

            //check and move Vertically
            potentialTransform.Translate(new Vector3(0.0f, 0.0f, Input.GetAxis("Vertical")));

            ray = new Ray(potentialTransform.position, Vector3.down);

            if (Physics.Raycast(ray, out hitInfo, 100f, layerMask))
            {
                oldPostion = potentialTransform.position;
            }
            else
            {
                potentialTransform.position = oldPostion;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, potentialTransform.position, Time.deltaTime * smooth);
    }
}
