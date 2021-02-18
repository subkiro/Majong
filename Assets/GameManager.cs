using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;
    public LevelSO CurrentLevel;

    //--------------------- Assets
    public ListOfObjects listOfAssets;


    private void Awake()
    {
        i = this;
        DontDestroyOnLoad(this.gameObject);
    }

   

    public void LoadCurrentLevel(LevelSO lvl)
    {
        this.CurrentLevel = lvl;

        SceneManager.LoadScene(1);
        char[,] a = lvl.GetLevelArray();
        //LoadLevel

        for (int i = 0; i < lvl.x; i++)
        {
            string s = "";
            for (int j = 0; j < lvl.y; j++)
            {
                s += a[i,j];
            }
           // Debug.Log(s);
        }
    }
}
