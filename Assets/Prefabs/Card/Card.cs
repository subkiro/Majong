using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card : MonoBehaviour
{

    public int id;
    public bool isSelected = false;
    [SerializeField] private SpriteRenderer spriteRender_Tile;
    [SerializeField] private SpriteRenderer spriteRender_Face;

    public GenericGrid<CardGrid> grid;
    public CardGrid CardGrid;
    public void InitCard(int id, Sprite face,Sprite tile,CardGrid cardGrid)
    {
        this.id = id;
        this.spriteRender_Face.sprite = face;
        this.spriteRender_Tile.sprite = tile;
        this.CardGrid = cardGrid;
    }



    public void OnMouseDown()
    {
        isSelected = !isSelected;
        Debug.Log("Selected Card ID: " + this.id + " x=" + this.CardGrid.x + " y=" + this.CardGrid.y);
        EventHandler.i.Select(this,this);

    }
}








public class CardGrid
{
    public GenericGrid<CardGrid> grid;
    public int x, y;
    public Card card;

    public CardGrid(GenericGrid<CardGrid> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }
    public void SetValue(Card card)
    {
        this.card = card;
        grid.TriggerGridObject(grid, x, y);
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