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
    
    private List<GameObject> nextTrucks = new List<GameObject>();
    private int listIndex = 0;
    private float timeNow = 0f; 
    private float timeNext = 0f;

    private bool initialised = false;

    public void Start()
    {
        genTrucksToSpawn();
        GameObject goal = GameObject.Find("Goal");
        goal.GetComponent<WinDetection>().allTrucks = nextTrucks.ToArray();
        goal.GetComponent<WinDetection>().initStuff();
        //initialised = true;
    }

    private void genTrucksToSpawn()
    {
        for(int i = 0; i<20; i++)
        {

            GameObject newTruck = Instantiate(prefab, spawnLoc, Quaternion.identity);
            newTruck.SetActive(false);
            Renderer rend = newTruck.GetComponent<Renderer>();
            rend.material = materials[Random.Range(0, materials.Length - 1)];
            nextTrucks.Add(newTruck);
        }
    }

	void FixedUpdate ()
    {
        //if (!initialised)
        //{
        //    return;
        //}
        //update time
        timeNow += Time.deltaTime;
		//check time
        if (timeNext < timeNow)
        {
            if (Physics.OverlapBox(spawnLoc, new Vector3(5f,0f, 5f)).Length == 0 && listIndex < 20)
            {
                //create a new object with random colour
                nextTrucks[listIndex].SetActive(true);
                listIndex = listIndex + 1;
                //set new timeNext for the next spawn
                timeNext = timeNow + Random.Range(intervalMin, intervalMax);
            }
        }
    }
}
