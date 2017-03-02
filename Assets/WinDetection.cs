using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetection : MonoBehaviour {

    public string levelToLoad;
    public Canvas canvas;
    public int winByTruckX;
    public int sizeWin;

    private List<GameObject> winTruckList;
    private GameObject[] allTrucks;

    void Start()
    {
        //genWinTruckList();

    }

    private void genWinTruckList()
    {
        List<int> usedTrucks = new List<int>();
        int tempRandom = 0;
        bool duplicate;
        for (int i = 0; i < sizeWin; i++)
        {
            duplicate = true;
            while (duplicate)
            {
                tempRandom = Random.Range(0, winByTruckX);
                if (!usedTrucks.Contains(tempRandom))
                {
                    usedTrucks.Add(tempRandom);
                    duplicate = false;
                }
            }
            usedTrucks.Add(tempRandom);
        }
        foreach (int i in usedTrucks)
        {
            winTruckList.Add(allTrucks[i]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Train")
        {
            print("loading scene " + levelToLoad);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
