using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mahjong/Level")]

public class LevelSO : ScriptableObject
{
    public int levelID;
    public Sprite LevelPhoto;
    public bool isCompleted;
    [TextArea]
    public string level = " ";
    public int x, y;
    private char[,] arrayList;





    public void SetCompleted() {
        isCompleted = true;
    }

    public char[,] GetLevelArray() {

        string[] lines = level.Split('\n');

      

       


        x = lines.Length;
      
        y= lines[0].ToCharArray().Length-1;

       
        arrayList = new char[x, y];

        for (int row = 0; row < x; row++)
        {
            char[] lineChars =  lines[row].ToCharArray();
            for (int colum = 0; colum < y; colum++)
            {
                //Debug.Log(row + " " + colum);
                arrayList[row, colum] = lineChars[colum];
            }
        }
        
        //will implement later
        return arrayList;
    }


   
}
