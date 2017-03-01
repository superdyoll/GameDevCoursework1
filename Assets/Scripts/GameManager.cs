using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float levelStartDelay = 2f;
    public int currentLevelNumber = 1;

    private Text levelText;
    private GameObject levelImage;
    private bool doingSetup;
    

	// Use this for initialization
	void Start () {
		
	}

    private void OnLevelWasLoaded(int level)
    {
        print("Level loaded");
        InitGame();
    }

    private void InitGame()
    {
        doingSetup = true;

        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + currentLevelNumber;
        levelImage.SetActive(true);
        Invoke("HideLevelImage", levelStartDelay);
        print("Invoked");
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void HideLevelImage()
    {
        print("Hiding");
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public bool GetDoingSetup()
    {
        return doingSetup;
    }
    
}