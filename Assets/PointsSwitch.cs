using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSwitch : MonoBehaviour {

    public GameObject[] tracks;

    // Use this for initialization
    void Start () {
        tracks[1].gameObject.SetActive(false);
	}
    
    void OnMouseDown()
    {
        foreach (GameObject track in tracks)
        {
            track.gameObject.SetActive(!track.activeSelf);
        }
    }
}