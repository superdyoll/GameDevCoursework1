using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckGeneration : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    public float intervalMin = 5f;
    public float intervalMax = 15f;
    public Material[] materials;
    public GameObject prefab;
    public Vector3 startSpawnLoc;
    public Vector3 endSpawnLoc;

    private float timeSince = 0f; 
    private float timeNext = 0f;
    private Vector3 direction;

	void FixedUpdate () {
        timeSince += Time.deltaTime;
		//check time - random time
        //time for the next train
        if (timeNext > timeSince)
        {
            //create a new train

            //check space - fixed space
            direction = startSpawnLoc - endSpawnLoc;
            for (int i = 0; i <= direction.magnitude; i++)
            {
                Physics.OverlapBox(startSpawnLoc + i * direction, );
            }
            //create object - random colour
            //set new timeNext for the next spawn
            timeNext = timeNext + Random.Range(intervalMin, intervalMax);
        }
    }
}
