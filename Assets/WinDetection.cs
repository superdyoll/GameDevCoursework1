using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinDetection : MonoBehaviour {

    public string levelToLoad;
    public GameObject canvas;
    public int winByTruckX;
    public int sizeWin;

    public List<GameObject> winTruckList;
    private GameObject[] allTrucks;

    void Start()
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
        RectTransform myRect = panel.AddComponent<RectTransform>();
        myRect.anchorMax = new Vector2(1f, 1f);
        myRect.anchorMin = new Vector2(1f, 1f);
        myRect.pivot = new Vector2(1f, 1f);
        myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50f);
        myRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50 * winTruckList.Count);
        panel.AddComponent<CanvasRenderer>();
        panel.AddComponent<Image>();
        panel.AddComponent<HorizontalLayoutGroup>();
        panel.transform.parent = canvas.transform;
        //GameObject panel = canvas.transform.GetChild(0).gameObject;
        //panel.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50 * winTruckList.Count);

        for (int i = 0; i < winTruckList.Count; i++ )
        {
            GameObject newImageObj = new GameObject("Truck" + (i+1).ToString());
            newImageObj.AddComponent<RectTransform>();
            newImageObj.AddComponent<CanvasRenderer>();
            Image newImageComp = newImageObj.AddComponent<Image>();
            newImageComp.color = winTruckList[i].GetComponent<Renderer>().material.color;
            newImageObj.transform.parent = panel.transform;
            newImageObj.SetActive(true);
        }

        

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

    private void drawTrucksWanted()
    {

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
