using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    
    public int id;
    private bool isSelected;
    public int GridPosX;
    public int GridPosY;

    [SerializeField] private SpriteRenderer spriteRender_Tile;
    [SerializeField] private SpriteRenderer spriteRender_Face;

    public GenericGrid<CardGrid> grid;
    public void InitCard(int id, Sprite face,Sprite tile, GenericGrid<CardGrid> grid,int gridPosX,int gridPosY)
    {
        this.id = id;
        this.spriteRender_Face.sprite = face;
        this.spriteRender_Tile.sprite = tile;
        this.GridPosX = gridPosX;
        this.GridPosY = gridPosY;
        this.grid = grid;
    }



    public void OnMouseDown()
    {

        
        
         //   Debug.Log("Selected Card ID: " + this.id + " x=" + this.CardGrid.x + " y=" + this.CardGrid.y);
            EventHandler.i.Select(this,this);

    }

    public Vector2 GetGridPos() {
        return new Vector2(GridPosX, GridPosY);
    }


    public bool IsSelected
    {
        get { return isSelected; }
        set
        {
            if (value) {
               
                Hightlight(value);
            }

            else {
                
                Hightlight(value);
            }

            isSelected = value;
        }
    }
    public void Hightlight(bool isSelected) {

        if (isSelected)
        {
            this.transform.DOScale(new Vector3(1.1f, 1.1f, 0), 0.2f).SetEase(Ease.InOutFlash);
        }
        else {
            this.transform.DOScale(new Vector3(1f, 1f, 0), 0.2f).SetEase(Ease.InOutFlash);
        }
    }


    private void OnDestroy()
    {
        this.grid.GetGridObject(GridPosX, GridPosY).isNotEmpty = false;
        this.transform.DOKill();
    }
}








public class CardGrid
{
    public GenericGrid<CardGrid> grid;
    public int x, y;
    public Card card;
    public bool isNotEmpty = false;

    public CardGrid(GenericGrid<CardGrid> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        isNotEmpty = true;

    }

    public CardGrid()
    {
        isNotEmpty = false;
    }
    public void SetValue(Card card)
    {
        if (card != null)
        {
            isNotEmpty = true;
            this.card = card;
        }
        else {
            isNotEmpty = false;
        }
        
    }

    public bool isTheSame(Card c) {
        if (c.transform.position == this.card.transform.position)
            return true;
        else
            return false;
    }
    public override string ToString()
    {
        if (card != null)
        {
            return card.id.ToString();
        }
        else return default;
    }

}