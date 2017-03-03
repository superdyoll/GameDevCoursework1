using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMove : MonoBehaviour, ICube
{
    public GameObject truck;
    private Rigidbody rb;


    public float smooth = 3;
    public int layerMask = (1 << 8);

    private float direction;

    private GameObject[] linked;
    private TrainMove train;

    // Use this for initialization
    void Start()
    {
        rb = truck.GetComponent<Rigidbody>();
        linked = new GameObject[2];
        RemoveAllConnections();
    }

    private void FixedUpdate()
    {
        //vert is w/s
        //horiz is a/d
        if (linked[0] != null || linked[1] != null)
        {
            direction = train.GetDirection();
            if (direction != 0)
            {
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
                                TruckMove truckmove = hitInfoTruck.collider.gameObject.GetComponent<TruckMove>();
                                truckmove.AddConnection(truck, direction);
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
                    //Quaternion newRotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")), hitInfo.transform.forward);
                    //newRotation *= Quaternion.Euler(0, 90, 0);
                    Quaternion newRotation = Quaternion.LookRotation(hitInfoRotate.transform.right, hitInfoRotate.transform.up);
                    float yComponent = newRotation.eulerAngles.y;
                    if (Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) != 0)
                    {

                        //Debug.Log(Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) + " , ycomp: " + yComponent + " , old ycomp: " + transform.eulerAngles.y);

                    }
                    if (Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) >= 180)
                    {
                        float temp = yComponent - 180;
                        //Debug.Log("Old y: " + transform.rotation.eulerAngles.y + ", Newer y: " + yComponent + "Math1: " + Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) + "Math2:" + temp);
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
    }

    private bool AddConnection(GameObject link, int position)
    {
        if (linked[position] == null)
        {
            linked[position] = link;
        }
        else
        {
            return false;
        }
        AddTrain(link);
        return true;
    }

    public bool AddConnection(GameObject link, float direction)
    {
        if (direction > 0)
        {
            return AddConnectionLeft(link);
        }
        else
        {
            return AddConnectionRight(link);
        }
    }

    private bool AddTrain(GameObject link)
    {
        if (link.tag == "Train")
        {
            train = link.GetComponent<TrainMove>();
        }
        else
        {
            train = link.GetComponent<TruckMove>().GetTrain();
        }
        return true;
    }

    public bool AddConnectionLeft(GameObject leftLink)
    {
        print("Adding truck connection on left");
        return AddConnection(leftLink, 0);
    }

    public bool AddConnectionRight(GameObject rightLink)
    {
        print("Adding truck connection on right");
        return AddConnection(rightLink, 1);
    }

    public GameObject[] GetConnection()
    {
        print("Current connection is " + linked.ToString());
        return linked;
    }

    public bool RemoveConnection(GameObject link)
    {
        bool returnBool = false;
        if (linked[0] == link)
        {
            linked[0] = null;
            returnBool = true;
        }
        else if (linked[1] == link)
        {
            linked[1] = null;
            returnBool = true;
        }
        return returnBool;
    }

    public void RemoveAllConnections()
    {
        GameObject emptyGameObject = null;
        RemoveAllConnections(emptyGameObject);
    }

    public void RemoveAllConnections(GameObject caller)
    {
        foreach (GameObject link in linked)
        {
            if (link != null)
            {
                TruckMove localTruck = link.GetComponent<TruckMove>();
                if (localTruck != null && localTruck != caller)
                {
                    localTruck.RemoveAllConnections(truck);
                }
            }
        }
        linked[0] = null;
        linked[1] = null;
    }

    public TrainMove GetTrain()
    {
        return train;
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
