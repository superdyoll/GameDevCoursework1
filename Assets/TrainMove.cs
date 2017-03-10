using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour, ICube
{

    public GameObject train;
    //private Rigidbody rb;

    public float smooth = 3;
    public int layerMask = (1 << 8);
    private float direction;

    public GameObject[] linked;

    // Use this for initialization
    void Start()
    {
        linked = new GameObject[2];
        linked[0] = null;
        linked[1] = null;
        //rb = cube.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("SPAAAAACCEE");
            foreach (GameObject link in linked)
            {
                if (link != null)
                {
                    TruckMove truckMove = link.GetComponent<TruckMove>();
                    if (truckMove != null)
                    {
                        truckMove.RemoveAllConnections(train);
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Move left/right with <- and -> or 'a' and 'd'
        direction = Input.GetAxis("Horizontal");

        //Debug.Log(direction);
        if (direction != 0)
        {
            // Movement
            Vector3 horizonalPosition = transform.position + new Vector3(direction * smooth, 0);
            for (float i = -smooth; i <= smooth; i++)
            {
                Vector3 rayPosition = horizonalPosition + new Vector3(0, 0, i);

                Ray rayMove = new Ray(rayPosition, Vector3.down);
                RaycastHit hitInfoMove, hitInfoTruck;
                if (Physics.Raycast(rayMove, out hitInfoMove, 25f, layerMask))
                {
                    Vector3 angle = rayPosition - transform.position;
                    angle.Normalize();

                    Ray rayTruck = new Ray(rayPosition, angle);
                    if (Physics.Raycast(rayTruck, out hitInfoTruck, 7f))
                    {
                        if (hitInfoTruck.collider.gameObject.tag == "Truck")
                        {
                            GameObject truck = hitInfoTruck.collider.gameObject;
                            TruckMove truckmove = truck.GetComponent<TruckMove>();
                            if (direction < 0)
                            {
                                linked[0] = truck;
                            }
                            else
                            {
                                linked[1] = truck;
                            }
                            truckmove.AddConnection(train, direction);
                        }
                    }
                    else
                    {
                        transform.position = rayPosition;
                        break;
                    }
                }
            }

            // Rotation
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hitInfoRotate;

            if (Physics.Raycast(ray, out hitInfoRotate, 100f, layerMask))
            {
                Quaternion newRotation = Quaternion.LookRotation(hitInfoRotate.transform.right, hitInfoRotate.transform.up);
                float yComponent = newRotation.eulerAngles.y;
                if (Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) >= 180)
                {
                    yComponent = yComponent - 180;
                }
                //this if statement may be unecessary
                if (yComponent >= 360)
                {
                    yComponent = yComponent - 360;
                }
                newRotation = Quaternion.Euler(newRotation.eulerAngles.x, yComponent, newRotation.eulerAngles.z);
                transform.rotation = newRotation;
            }
        }
    }

    public float GetDirection()
    {
        return direction;
    }

    public GameObject GetTruckOnRight()
    {
        return linked[1];
    }

    public GameObject GetTruckOnLeft()
    {
        return linked[0];
    }
}