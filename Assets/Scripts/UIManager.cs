using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager instance;

    public GameObject PopUpPrefab,MainMenu,PopUpLost,PopUpExit;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this.gameObject);
        }

    }


   

    public void ShopPopUpWin(string message, Action  onClickOK) {
        GameObject t = Instantiate(PopUpPrefab);
        t.GetComponent<PopUpScript>().Init(message, onClickOK, () => Debug.Log("exeeeeedddddddd"));
       
    }

    public void ShopPopUpLost(string message, Action onClickOK)
    {
        GameObject t = Instantiate(PopUpLost);
        t.GetComponent<PopUpScript>().Init(message, onClickOK, () => Debug.Log("exeeeeedddddddd"));

    }
    public void ShopPopExit(string message, Action onClickOK)
    {
        GameObject t = Instantiate(PopUpExit);
        t.GetComponent<PopUpScript>().Init(message, onClickOK, () => Debug.Log("exeeeeedddddddd"));

    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        
        if (scene.buildIndex == 0)
        {
           
            GameObject t = Instantiate(MainMenu);
        }
    }

}
