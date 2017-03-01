using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerationScript : MonoBehaviour {

    private GameObject trackHolder;
    public Material trackMat;

    //public Vector3[] poses;

    public Vector3[][] allCorners = {
    new Vector3[] { new Vector3(-150f, 0f, 50f), new Vector3(-100f, 0f, 50f) } ,
    new Vector3[] { new Vector3(-100f, 0f, 50f), new Vector3(-90f, 0f, 50f), new Vector3(-90f, 0f, 60f) },
    new Vector3[] { new Vector3(-90f,0,50f), new Vector3(-40f,0,50f)},
    new Vector3[] { new Vector3(-30f,0,50f), new Vector3(-40f,0,50f), new Vector3(-40f, 0, 40f) },
    new Vector3[] { new Vector3(-90f,0,40f), new Vector3(-40f,0f,40f)},
    new Vector3[] { new Vector3(-30f,0f,50f), new Vector3(-20f,0,50f)},
    new Vector3[] { new Vector3(-20f,0f,50f), new Vector3(-10f,0,50f), new Vector3(-10f, 0, 40f) }
    };
    
	void Start () {
        trackHolder = new GameObject("TrackHolder");
        //Vector3[] positions = { new Vector3(100f, 0.1f, 100f), new Vector3(100f, 0.1f, 150f) };
        foreach (Vector3[] set in allCorners)
        {
            if (set.Length == 2)
            {
                drawStraight(set);
            }
            else
            {
                drawPoints(set);
            }
        }
	}

    GameObject drawStraight(Vector3[] positions)
    {
        GameObject newTrack = new GameObject("Track");
        LineRenderer newTrackLineRender = newTrack.AddComponent<LineRenderer>();
        newTrackLineRender.SetPositions(positions);
        newTrackLineRender.material = trackMat;
        newTrackLineRender.startWidth = 1;
        newTrackLineRender.endWidth = 1;
        Vector3 centreOfLine = (positions[0] + positions[1]) / 2;
        newTrack.transform.position = centreOfLine;
        BoxCollider myBox = newTrack.AddComponent<BoxCollider>();
        if (positions[0].z != positions[1].z)
        {
            Vector3 lineDirection = positions[1] - positions[0];
            //TODO: fix this!, make sure the boxes work in all 4 directions
            float sign = (Math.Abs(positions[1].z)) < (Math.Abs(positions[0].z)) ? 1.0f : -1.0f;
            float angle = Vector3.Angle(lineDirection.normalized, Vector3.right) * sign;
            newTrack.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
        myBox.center = Vector3.zero;
        float lineSize = (positions[1] - positions[0]).magnitude;
        myBox.size = new Vector3(lineSize, 1, 1);
        newTrack.layer = 8;
        newTrack.transform.parent = trackHolder.transform;
        return newTrack;
    }

    void drawPoints(Vector3[] positions)
    {
        Vector3[] firstVectorPair = {positions[0], positions[1] };
        Vector3[] secondVectorPair = {positions[0], positions[2] };
        GameObject track1 = drawStraight(firstVectorPair);
        GameObject track2 = drawStraight(secondVectorPair);
        GameObject newButton = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        newButton.name = "Button";
        newButton.transform.localScale = new Vector3(10f, 0.001f, 10f);
        //slightly dodgy.
        //TODO: change to get largest value from 1 and 2 which does no clode with track.
        newButton.transform.position = positions[0] + positions[2] - positions[1];
        newButton.layer = 9;
        newButton.GetComponent<Renderer>().material.color = Color.red;
        PointsSwitch pointsScript = newButton.AddComponent<PointsSwitch>();
        pointsScript.tracks = new GameObject[] { track1, track2 };
    }
}