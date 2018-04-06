using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class WinDetection : MonoBehaviour
{

    public string levelToLoad;
    public int winByTruckX = 0;
    public int sizeWin = 0;

    public bool justTrain = false;

    public GameObject[] allTrucks;

    private List<GameObject> winTruckList = new List<GameObject>();
    private RectTransform myRect;
    private GameObject canvas;

    private bool inited = false;

    void Start()
    {
        initStuff();
    }

    public void initStuff()
    {
        if (allTrucks.Length != 0 && sizeWin != 0 && winByTruckX != 0 && !inited)
        {
            inited = true;
            genWinTruckList();
            drawTrucksWanted();
            genTruckPlanes();
        }
        if (!justTrain)
        {
            gameObject.transform.localScale = new Vector3(sizeWin * 1f + 1.5f, 1, 1.5f);
            gameObject.transform.position = gameObject.transform.position + new Vector3(-10f, 0f, 0f);
        }
    }
 
    private void genTruckPlanes()
    {
        int half = winTruckList.Count / 2;
        int mod = winTruckList.Count % 2;
        Vector3 centre = gameObject.transform.position;
        Vector3 gapBetweenBoxes = new Vector3(15f, 0f, 0f);
        Vector3 offset = new Vector3(7.5f, 0f, 0f);

        if (mod == 0)
        {
            //length is even
            float halfF = half - 0.5f;
            for (int i = 0; i < half; i++)
            {
                for (int j = -1; j < 2; j = j + 2)
                {
                    GameObject newTruckPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    newTruckPlane.transform.localScale = new Vector3(1.5f, 1f, 1.5f);
                    newTruckPlane.name = "EVEN";
                    newTruckPlane.GetComponent<Renderer>().material = winTruckList[(int) (halfF + (j *(i + 0.5f)))].GetComponent<Renderer>().material;
                    newTruckPlane.transform.position = j * (gapBetweenBoxes * i + offset) + centre;
                    Debug.Log(j * (gapBetweenBoxes * i));
                }
            }
            //offset first loc by 7.5f
        }
        else
        {
            //length is odd
            for (int i = 0; i <= half; i++)
            {
                for (int j = -1; j < 2; j = j + 2)
                {
                    GameObject newTruckPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
                    newTruckPlane.transform.localScale = new Vector3(1.5f, 1f, 1.5f);
                    newTruckPlane.name = "ODD";
                    newTruckPlane.GetComponent<Renderer>().material = winTruckList[half + (j * i)].GetComponent<Renderer>().material;
                    newTruckPlane.transform.position = j * (gapBetweenBoxes * i) + centre;
                }
            }
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;




        //if (mod == 0)
        //{
        //    offsetLoc = 7.5f;

        //}
        //else
        //{
        //    GameObject newTruckPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //    newTruckPlane.name = "ASDFGHJKL";
        //    newTruckPlane.GetComponent<Renderer>().material = winTruckList[(int)half].GetComponent<Renderer>().material;
        //}

        //for (int i = 1; i < half; i++)
        //{
        //    for (int j = -1; j < 2; j = j + 2)
        //    {
        //        GameObject newTruckPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //        newTruckPlane.name = "ASDFGHJKL";
        //        newTruckPlane.GetComponent<Renderer>().material = winTruckList[(int) half + (j * i)].GetComponent<Renderer>().material;
        //        newTruckPlane.transform.position = new Vector3((j * (gapBetweenBoxes.x * i + offsetLoc)) + centre.x, centre.y, centre.z);
        //    }
        //}
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
        panel.transform.SetParent(canvas.transform);

        for (int i = 0; i < winTruckList.Count; i++)
        {
            GameObject newImageObj = new GameObject("Truck" + (i + 1).ToString());
            newImageObj.AddComponent<RectTransform>();
            newImageObj.AddComponent<CanvasRenderer>();
            Image newImageComp = newImageObj.AddComponent<Image>();
            newImageComp.color = winTruckList[i].GetComponent<Renderer>().material.color;
            newImageObj.transform.SetParent(panel.transform);
            newImageObj.SetActive(true);
        }
        myRect.anchoredPosition = new Vector3(0, 0, 0);
    }

    void OnTriggerExit(Collider other)
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Train")
        {
            Debug.Log("TRAIN!!");
            if (!justTrain)
            {
                GameObject truck = other.GetComponent<TrainMove>().GetTruckOnRight();

                for (int i = 0; i < sizeWin; i++)
                {
                    if (truck == null)
                    {
                        Debug.Log("No truck(" + i + ")");
                        return;
                    }
                    if (winTruckList[i].GetComponent<Renderer>().material.name != truck.GetComponent<Renderer>().material.name)
                    {
                        Debug.Log("Wrong coloured truck");
                        Debug.Log("Expected: " + winTruckList[i].GetComponent<Renderer>().material.name);
                        Debug.Log("Got: " + truck.GetComponent<Renderer>().material.name);

                        return;
                    }
                    else
                    {
                        Debug.Log("Truck " + i);
                        truck = truck.GetComponent<TruckMove>().GetTruckOnRight();
                    }
                }
            }

            print("loading scene " + levelToLoad);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}