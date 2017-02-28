using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDetection : MonoBehaviour {

    public string levelToLoad;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Train")
        {
            print("loading scene " + levelToLoad);
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
