using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMove : MonoBehaviour
{
    public GameObject cube;
    private Rigidbody rb;


    public float smooth = 3;
    public int layerMask = (1 << 8);

    // Use this for initialization
    void Start()
    {
        rb = cube.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //vert is w/s
        //horiz is a/d
        if (rb.velocity != Vector3.zero)
        {
            Vector3 horizonalPosition = transform.position + new Vector3(rb.velocity.x * smooth, 0);
            for (float i = -smooth; i <= smooth; i++)
            {
                Vector3 rayPosition = horizonalPosition + new Vector3(0, 0, i);
                Ray rayMove = new Ray(rayPosition, Vector3.down);
                RaycastHit hitInfoMove;
                if (Physics.Raycast(rayMove, out hitInfoMove, 25f, layerMask))
                {
                    transform.position = rayPosition;
                    break;
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

                    Debug.Log(Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) + " , ycomp: " + yComponent + " , old ycomp: " + transform.eulerAngles.y);

                }
                if (Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) >= 180)
                {
                    float temp = yComponent - 180;
                    Debug.Log("Old y: " + transform.rotation.eulerAngles.y + ", Newer y: " + yComponent + "Math1: " + Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) + "Math2:" + temp);
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
