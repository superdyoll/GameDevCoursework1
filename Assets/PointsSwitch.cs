using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsSwitch : MonoBehaviour {

    public GameObject[] tracks;

    // Use this for initialization
    void Start () {
		
	}
    
    void OnMouseDown()
    {
        foreach (GameObject track in tracks)
        {
            track.SetActive(!track.activeSelf);
        }
    }
}