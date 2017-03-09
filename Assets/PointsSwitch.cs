using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSwitch : MonoBehaviour {

    public GameObject[] tracks;

    // Use this for initialization
    void Start () {
        if (tracks[0].gameObject.activeSelf)
        {
            tracks[1].gameObject.SetActive(false);
        }else
        {
            tracks[1].gameObject.SetActive(true);
        }
	}
    
    void OnMouseDown()
    {
        bool obstructed = false;
        foreach (GameObject track in tracks)
        {

            foreach (Collider obsticles in Physics.OverlapBox(track.transform.position + Vector3.up * 5, new Vector3(track.GetComponent<BoxCollider>().size.x / 2, 0f, 1f), track.transform.rotation))
            {
                if (obsticles.tag == "Train" || obsticles.tag == "Truck")
                {
                    obstructed = true;
                }
            }
            if (!obstructed)
            {
                track.gameObject.SetActive(!track.activeSelf);
            }
        }
    }
}