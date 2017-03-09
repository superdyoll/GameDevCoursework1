using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class WinDetection : MonoBehaviour {

    public string levelToLoad;
    public GameObject canvas;
    public int winByTruckX;
    public int sizeWin;

    public List<GameObject> winTruckList;
    private GameObject[] allTrucks;
    private RectTransform myRect;

    void Start()
    {
        drawTrucksWanted();
        //genWinTruckList();
        gameObject.transform.localScale = new Vector3(1.5f,1f,1.5f);
        genTruckPlanes();
    }

    private void genTruckPlanes()
    {
        int half = winTruckList.Count/2;
        int mod = winTruckList.Count % 2;
        if (mod == 0)
        {
            //a ting
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = winTruckList[half].GetComponent<Renderer>().material;
            Vector3 oldGameObjectLoc = gameObject.transform.position;
            Vector3 gapBetweenBoxes = new Vector3(15f, 0f, 0f);
            for (int i = 1; i < half; i++)
            {
                for (int j = -1; j < 2; j = j + 2)
                {
                    GameObject newTruckPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    newTruckPlane.name = "ASDFGHJKL";
                    newTruckPlane.GetComponent<Renderer>().material = winTruckList[half + (j * i)].GetComponent<Renderer>().material;
                    newTruckPlane.transform.position = oldGameObjectLoc + (j * gapBetweenBoxes);
                }
            }
        }
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
        //if(other.gameObject.tag == "Train")
        //{
        //    print("loading scene " + levelToLoad);
        //    SceneManager.LoadScene(levelToLoad);
        //}
    }
}