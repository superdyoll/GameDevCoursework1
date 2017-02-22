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

	void FixedUpdate ()
    {
        //update time
        timeSince += Time.deltaTime;
		//check time
        if (timeNext < timeSince)
        {
            //    Debug.Log("Time passed");
            //check space - fixed space
            direction = startSpawnLoc - endSpawnLoc;
            Debug.Log(direction);
            for (int i = 0; i <= direction.magnitude; i++)
            {
                if (Physics.OverlapBox(startSpawnLoc + i * direction.normalized, new Vector3(5f,0f, 5f)).Length == 0)
                {
                    Debug.Log(i);
                    //create a new object with random colour
                    GameObject newTruck = Instantiate(prefab, startSpawnLoc + i * direction.normalized, Quaternion.identity);
                    Renderer rend = newTruck.GetComponent<Renderer>();
                    rend.material = materials[Random.Range(0, materials.Length - 1)];
                    //set new timeNext for the next spawn
                    timeNext = timeNext + Random.Range(intervalMin, intervalMax);
                    break;
                }
            }
        }
    }
}
