using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;

    private Vector3 offset;

    private Vector3 wantedPosition;

    public float smooth = 1;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;

        float speed = velocity.magnitude;

        velocity.Normalize();

        wantedPosition = player.transform.position - 4 * velocity + new Vector3(0, offset.y, 0);

        transform.position = Vector3.MoveTowards(transform.position, wantedPosition, Time.deltaTime * smooth * speed);
        transform.LookAt(player.transform);
    }
}

