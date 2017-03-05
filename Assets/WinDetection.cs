﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinDetection : MonoBehaviour {

    public string levelToLoad;
    public int winByTruckX = 0;
    public int sizeWin = 0;

    public GameObject[] allTrucks;

    private List<GameObject> winTruckList = new List<GameObject>();
    private RectTransform myRect;
    private GameObject canvas;

    void Start()
    {
        Debug.Log(allTrucks.Length);
        if (allTrucks.Length != 0 && sizeWin != 0 && winByTruckX != 0)
        {
            genWinTruckList();
            drawTrucksWanted();
        }
       
        gameObject.transform.localScale = new Vector3(sizeWin*1.5f,1,1.5f);
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
        }
        foreach (int i in usedTrucks)
        {
            winTruckList.Add(allTrucks[i]);
        }
    }

    private void drawTrucksWanted()
    {
        GameObject mainCam = GameObject.Find("Main Camera");
        canvas = new GameObject("CanvasUI");
        canvas.transform.parent = mainCam.transform;
        canvas.AddComponent<RectTransform>();
        Canvas myCanvas = canvas.AddComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        GameObject panel = new GameObject("Panel Display");
        myRect = panel.AddComponent<RectTransform>();
        myRect.anchorMax = new Vector2(1f, 1f);
        myRect.anchorMin = new Vector2(1f, 1f);
        myRect.pivot = new Vector2(1f, 1f);
        myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50f);
        myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50 * winTruckList.Count);
        myRect.anchoredPosition = new Vector3(0, 0, 0);

        panel.AddComponent<CanvasRenderer>();
        panel.AddComponent<Image>();
        panel.AddComponent<HorizontalLayoutGroup>();
        panel.transform.parent = canvas.transform;

        for (int i = 0; i < winTruckList.Count; i++)
        {
            GameObject newImageObj = new GameObject("Truck" + (i + 1).ToString());
            newImageObj.AddComponent<RectTransform>();
            newImageObj.AddComponent<CanvasRenderer>();
            Image newImageComp = newImageObj.AddComponent<Image>();
            newImageComp.color = winTruckList[i].GetComponent<Renderer>().material.color;
            newImageObj.transform.parent = panel.transform;
            newImageObj.SetActive(true);
        }

        myRect.anchoredPosition = new Vector3(0, 0, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Train")
        {

            GameObject truck = other.GetComponent<TrainMove>().GetTruckOnRight();

            for (int i = 0; i < sizeWin; i++)
            {
                if (winTruckList[i].GetComponent<Renderer>().material != truck.GetComponent<Renderer>().material)
                {
                    return;
                }
                else if(i != (sizeWin -1))
                {
                    truck = truck.GetComponent<TruckMove>().GetTruckOnRight();
                }
            }

            print("loading scene " + levelToLoad);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
