using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;

    public float rotationDamping = 3.0f;

	// Use this for initialization
	void Start () {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate () {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        float wantedRotationAngle = player.transform.eulerAngles.y;
        //float currentRotationAngle = this.transform.eulerAngles.y;
        // Damp the rotation around the y-axis
       //currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
       // Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        transform.position = player.transform.position + offset;
        //transform.rotation = currentRotation;
    }
}

