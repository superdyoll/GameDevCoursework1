﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackGenerationScript : MonoBehaviour {

    private GameObject trackHolder;
    public Material trackMat;
    public int mapNo;
    public Material OffColorMaterial;

    //jagged 2d array of all pieces of track and points
    private Vector3[][] map1 =
    {
        new Vector3[] {new Vector3(-200f,0f, 0f), new Vector3(200f,0f,0f) } 
    };
    private Vector3[][] map2 =
    {
        new Vector3[] {new Vector3(-200f,0f, 0f), new Vector3(200f,0f,0f) }
    };
    private Vector3[][] map3 =
    {
        new Vector3[] {new Vector3(-200f,0f, 0f), new Vector3(-25f,0f,0f) },
        new Vector3[] {new Vector3(-25f,0f,0f), new Vector3(0f,0f,0f), new Vector3(0f,0f,25f) },
        new Vector3[] {new Vector3(0f,0f,0f), new Vector3(200f,0f, 0f)  },
        new Vector3[] {new Vector3(0f, 0f, 25f), new Vector3(25f, 0f, 25f)  }   
    };

    private Vector3[][] map4 =
    {
        new Vector3[] { new Vector3(-199f, 0f, 0f), new Vector3(0f, 0f, 0f) },
        new Vector3[] { new Vector3(0f, 0f, 0f), new Vector3(15f, 0f, 0f), new Vector3(15f, 0f, 15f) },
        new Vector3[] { new Vector3(15f, 0f, 0f), new Vector3(200f, 0f, 0f) },
        new Vector3[] { new Vector3(15f,0f, 15f), new Vector3(25f, 0f, 15f) },
        new Vector3[] { new Vector3(25f, 0f, 15f), new Vector3(40f, 0f, 15f), new Vector3(40f, 0f, 30f) },
        new Vector3[] { new Vector3(40f, 0f, 15f), new Vector3(60f, 0f, 15f)},
        new Vector3[] { new Vector3(40f, 0f, 30f), new Vector3(60f, 0f, 30f)}
    };

    private Vector3[][] map5 =
    {
        new Vector3[] { new Vector3(-200f, 0f, 0f), new Vector3(-25f, 0f, 0f) } ,
        new Vector3[] { new Vector3(-25f, 0f, 0f), new Vector3(-10f, 0f, 0f), new Vector3(-10f, 0f, 15f) } ,
        new Vector3[] { new Vector3(-10f, 0f, 0f), new Vector3(20f, 0f, 0f)},
        new Vector3[] { new Vector3(-10f, 0f, 15f), new Vector3(5f, 0f, 15f), new Vector3(5f, 0f, 30f) } ,
        new Vector3[] { new Vector3(5f,0f,30f), new Vector3(40f,0f,30f),  }, 
        new Vector3[] { new Vector3(5f,0f, 15f), new Vector3(20f, 0f, 15f) }, 
        new Vector3[] { new Vector3(35f, 0f, 0f), new Vector3(20f, 0f, 0f), new Vector3(20f, 0f, 15f) } ,
        new Vector3[] { new Vector3(35f, 0f, 0f), new Vector3(200f, 0f, 0f)}, 
    };

    private Vector3[][] map6 =
    {
        new Vector3[] { new Vector3(-200f, 0f, 0f), new Vector3(-70f, 0f, 0f) } ,
        new Vector3[] { new Vector3(-70f, 0f, 0f), new Vector3(-50f, 0f, 0f), new Vector3(-50f, 0f, -20f) } ,
        new Vector3[] { new Vector3(-50f, 0f, 0f), new Vector3(-30f,0f,0f) } ,
        new Vector3[] { new Vector3(-50f, 0f, -20f), new Vector3(-30f, 0f, -20f) } ,
        new Vector3[] { new Vector3(-10f, 0f, 0f), new Vector3(-30f, 0f, 0f), new Vector3(-30f, 0f, -20f) } ,
        new Vector3[] { new Vector3(-10f, 0f, 0f), new Vector3(10f, 0f, 0f), new Vector3(10f, 0f, 20f) } ,
        new Vector3[] { new Vector3(10f, 0f, 0f), new Vector3(30f,0f, 0f) } ,
        new Vector3[] { new Vector3(10f, 0f, 20f), new Vector3(30f,0f, 20f) } ,
        new Vector3[] { new Vector3(50f,0f,0f), new Vector3(30f, 0f, 0f), new Vector3(30f, 0f, 20f) } ,
        new Vector3[] { new Vector3(50f, 0f, 0f), new Vector3(200f, 0f, 0f)}
    };

    private Vector3[][] testMap =
    {
        new Vector3[] { new Vector3(-150f, 0f, 50f), new Vector3(-100f, 0f, 50f) } ,
        new Vector3[] { new Vector3(-100f, 0f, 50f), new Vector3(-90f, 0f, 50f), new Vector3(-90f, 0f, 60f) },
        new Vector3[] { new Vector3(-90f, 0f, 50f), new Vector3(-40f, 0f, 50f) },
        new Vector3[] { new Vector3(-30f, 0f, 50f), new Vector3(-40f, 0f, 50f), new Vector3(-40f, 0f, 40f) },
        new Vector3[] { new Vector3(-90f, 0f, 40f), new Vector3(-40f, 0f, 40f) },
        new Vector3[] { new Vector3(-30f, 0f, 50f), new Vector3(-20f, 0f, 50f) },
        new Vector3[] { new Vector3(-20f, 0f, 50f), new Vector3(-10f, 0f, 50f), new Vector3(-10f, 0f, 40f) }
    };

    public List<Vector3[][]> maps = new List<Vector3[][]>();

	void Start () {
        //place mapLayouts into maps var.
        maps.Add(map1);
        maps.Add(map2);
        maps.Add(map3);
        maps.Add(map4);
        maps.Add(map5);
        maps.Add(map6);
        //create an empty gameobject to hold all the track
        trackHolder = new GameObject("TrackHolder");
        //for each array in the array draw the track, if the array is 3 long then its a set of points, if 2 then its a straight
        foreach (Vector3[] set in maps[mapNo])
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
            float sign = positions[1].z < positions[0].z ? 1.0f : -1.0f;
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
        //TODO: change to get largest value from 1 and 2 which does no colide with track.
        newButton.transform.position = positions[0] + positions[2] - positions[1];
        newButton.layer = 9;
        newButton.GetComponent<Renderer>().material.color = Color.red;
        PointsSwitch pointsScript = newButton.AddComponent<PointsSwitch>();
        pointsScript.OffColor = OffColorMaterial;
        pointsScript.OnColor = trackMat;
        pointsScript.Tracks = new PointsSwitch.TracksClass[] { new PointsSwitch.TracksClass(track1), new PointsSwitch.TracksClass(track2) };
    }
}