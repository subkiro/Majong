using System;
using System.Collections.Generic;
using UnityEngine;

public class GenericGrid<TGridObject>
{
    public int width = 5;
    public int height = 5;
    public float cellSize;
    private Vector3 originPosition;
    private TGridObject[,] gridArray;
    private TextMesh[,] textMeshArray;

    //event 
    public delegate void OnGridObjectChanged(int x, int y);
    public event OnGridObjectChanged OnGridObjectChangedEvent;




    //constuctor
    public GenericGrid(int width, int height, float cellSize, Vector3 originPosition, Func<GenericGrid<TGridObject>,int,int,TGridObject> createGridObject, bool ShowGrid = false)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new TGridObject[this.width, this.height];
        textMeshArray = new TextMesh[this.width, this.height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = createGridObject(this,x,y);
            }
        }



                if (ShowGrid) {

                        for (int x = 0; x < gridArray.GetLength(0); x++)
                        {
                            for (int y = 0; y < gridArray.GetLength(1); y++)
                            {
                            //Create Visual
                   
                            //move visuals to the center of the cell grid
                             Vector3 offcet = new Vector3(cellSize, cellSize) * 0.5f;   
                                        
                             textMeshArray[x,y] =  Subkiro.CreateTextMesh(gridArray[x,y]?.ToString(),null, GetWorldPosition(x, y) + offcet);
                            //draw the lines to see the grid 
                             Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1),Color.white,100f);
                             Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x+1, y), Color.white, 100f);
                             }
               
                         }
                        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
                        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

                        OnGridObjectChangedEvent += (myX, myY) => { 
                            textMeshArray[myX, myY].text = gridArray[myX, myY]?.ToString(); 
                 };
        }


    }

    internal void TriggerGridObject(GenericGrid<TGridObject> grid, int x, int y)
    {
       // OnGridObjectChangedEvent(x, y);
    }

    public void SetGridObject(int x, int y, TGridObject value) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            gridArray[x, y] = value;
            textMeshArray[x, y].text = value.ToString(); ;
            if (OnGridObjectChangedEvent != null) {
                OnGridObjectChangedEvent(x, y);
            }
        }
       
    }

    public Vector3 GetWorldPosition(int x, int y) {
        return new Vector3(x, y) * cellSize + originPosition;
    }
    
    
    private void GetXY(Vector3 worldPosition, out int x, out int y) {
        x = Mathf.FloorToInt((worldPosition - originPosition).x  / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y/ cellSize);
    }
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetGridObject(x, y, value);

    }


    public TGridObject GetGridObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];

        }
        else {
            return default(TGridObject);
        }
    }


    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetGridObject(x, y);
    }
}