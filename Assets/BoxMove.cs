using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        //vert is w/s
        //horiz is a/d

        Vector3 oldPostion = transform.position;
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
        //Something something something and stuff and things;
    }
}
