using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckGeneration : MonoBehaviour {

    // Update is called once per frame
    public float intervalMin = 5f;
    public float intervalMax = 15f;
    public Material[] materials;
    public GameObject prefab;
    public Vector3 spawnLoc;

    private float timeNow = 0f; 
    private float timeNext = 0f;

	void FixedUpdate ()
    {
        //update time
        timeNow += Time.deltaTime;
		//check time
        if (timeNext < timeNow)
        {
            if (Physics.OverlapBox(spawnLoc, new Vector3(5f,0f, 5f)).Length == 0)
            {
                //create a new object with random colour
                GameObject newTruck = Instantiate(prefab, spawnLoc, Quaternion.identity);
                Renderer rend = newTruck.GetComponent<Renderer>();
                rend.material = materials[Random.Range(0, materials.Length - 1)];
                //set new timeNext for the next spawn
                timeNext = timeNow + Random.Range(intervalMin, intervalMax);
            }
        }
    }
}
