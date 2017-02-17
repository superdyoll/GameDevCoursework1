using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour {

    public GameObject cube;
    private Rigidbody rb;


    public float smooth = 3;
    public int layerMask = (1 << 8);

    // Use this for initialization
    void Start () {
		rb = cube.GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        //vert is w/s
        //horiz is a/d

        float direction = Input.GetAxis("Horizontal");
        if (direction != 0)
        {
            Vector3 horizonalPosition = transform.position + new Vector3(direction * smooth, 0);
            for (float i = -smooth; i <= smooth; i++)
            {
                Vector3 rayPosition = horizonalPosition + new Vector3(0, 0, i);
                Ray rayMove = new Ray(rayPosition, Vector3.down);
                RaycastHit hitInfoMove;
                if (Physics.Raycast(rayMove, out hitInfoMove, 100f, layerMask))
                {
                    transform.position = rayPosition;
                    break;
                }
            }

            Ray rayRotate = new Ray(transform.position, Vector3.down);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayRotate, out hitInfo, 100f, layerMask))
            {
                //Quaternion newRotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")), hitInfo.transform.forward);
                //newRotation *= Quaternion.Euler(0, 90, 0);
                Quaternion temp = Quaternion.LookRotation(hitInfo.transform.right, hitInfo.transform.up);
                transform.rotation = temp;
            }
        }
    }

}
