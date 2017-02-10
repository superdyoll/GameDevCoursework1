using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;

    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;
    public float distanceStandard = 5.0f;

   // private Rigidbody rigidbody;
    private Vector3 offset;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        //if (rigidbody != null)
        //{
        //    rigidbody.freezeRotation = true;
        //}

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player)
        {
           

            Vector3 velocity = player.GetComponent<Rigidbody>().velocity;

            velocity.Normalize();
            
            x += velocity.x * xSpeed * distance * 0.02f;
            y -= velocity.y * ySpeed * 0.02f;

            //y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(0,y,0);

            distance = distanceStandard;

            RaycastHit hit;
            if (Physics.Linecast(player.transform.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + player.transform.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}

