using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove : MonoBehaviour {

    //public GameObject cube;
    //private Rigidbody rb;


    public float smooth = 3;
    public int layerMask = (1 << 8);

    // Use this for initialization
    void Start () {
		//rb = cube.GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        // Move left/right with <- and -> or 'a' and 'd'

        float direction = Input.GetAxis("Horizontal");
        Debug.Log(direction);
        if (direction != 0)
        {
            // Movement
            Vector3 horizonalPosition = transform.position + new Vector3(direction * smooth, 0);
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
                Quaternion newRotation = Quaternion.LookRotation(hitInfoRotate.transform.right, hitInfoRotate.transform.up);
                float yComponent = newRotation.eulerAngles.y;
                if (Mathf.Abs(yComponent - transform.rotation.eulerAngles.y) >= 180)
                {
                    float temp = yComponent - 180;
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