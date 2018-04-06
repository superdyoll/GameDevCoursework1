using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for attaching to a set of points, which will switch them when a button is pressed.
public class PointsSwitch : MonoBehaviour
{
    public GameObject[] TrackGameObjects;
    public TracksClass[] Tracks;
    public Material OffColor;
    public Material OnColor;

    // Use this for initialization
    public void Start()
    {
        TrackGameObjects = new GameObject[2];
        if (TrackGameObjects[0] != null)
        {
            Tracks[0] = new TracksClass(TrackGameObjects[0]);
            Tracks[1] = new TracksClass(TrackGameObjects[1]);
        }
        Deactivate(Tracks[0]);
        Activate(Tracks[1]);
    }

    public void OnMouseDown()
    {
        //check if anything is over the points - obstructing them
        bool obstructed = false;
        foreach (TracksClass track in Tracks)
        {
            foreach (
                Collider obsticles in
                Physics.OverlapBox(track.gameObject.transform.position + Vector3.up * 5,
                    new Vector3((track.gameObject.GetComponent<BoxCollider>().size.x / 2) * 0.8f, 0f, 1f),
                    track.gameObject.transform.rotation))
            {
                if (obsticles.tag == "Train" || obsticles.tag == "Truck")
                {
                    obstructed = true;
                }
            }
        }
        //if the points are not obstructed the remove active tracks and active deactivate tracks
        if (!obstructed)
        {
            Invert(Tracks[0]);
            Invert(Tracks[1]);
        }
    }

    //method which deactives a piece of track if it is active or activaties it if it is not active.
    private void Invert(TracksClass track)
    {
        if (track.enabled)
        {
            Deactivate(track);
        }
        else
        {
            Activate(track);
        }
    }

    //to activate a piece of track, add it to the track layer (8), and make it the right colour and size.
    private void Activate(TracksClass track)
    {
        track.gameObject.GetComponent<Renderer>().material = OnColor;
        track.gameObject.GetComponent<LineRenderer>().startWidth = 1;
        track.gameObject.GetComponent<LineRenderer>().endWidth = 1;
        track.gameObject.layer = 8;
        track.enabled = true;
    }


    //to deactivate a piece of track, remove it from the track layer (8), and make it the right colour and size.
    private void Deactivate(TracksClass track)
    {
        track.gameObject.GetComponent<Renderer>().material = OffColor;
        track.gameObject.GetComponent<LineRenderer>().startWidth = 2;
        track.gameObject.GetComponent<LineRenderer>().endWidth = 2;
        track.gameObject.layer = 0;
        track.enabled = false;
    }

    //a local class representing a piece of track.
    public class TracksClass
    {
        public GameObject gameObject { get; private set; }
        public bool enabled { get; set; }

        public TracksClass(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}