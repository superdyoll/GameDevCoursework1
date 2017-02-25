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
        foreach (GameObject track in tracks)
        {
            track.gameObject.SetActive(!track.activeSelf);
        }
    }
}