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
        if (i != null && i != this)
            Destroy(this.gameObject);
        else
        {
            i = this;
            DontDestroyOnLoad(this.gameObject);
        }
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0) {
            UIManager.instance.ShopPopExit("Exit to main menu?", () => {
                UIManager.instance.ShopPopUpLost("You are lost lvl "+CurrentLevel.levelID, ()=> SceneManager.LoadScene(0));
             });
            
        }
    }
}
