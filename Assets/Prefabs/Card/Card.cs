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
    [SerializeField] private SpriteRenderer spriteRender_Selected;
    public GenericGrid<CardGrid> grid;
    public void InitCard(int id, Sprite face,Sprite tile, GenericGrid<CardGrid> grid,int gridPosX,int gridPosY)
    {
        this.id = id;
        this.spriteRender_Face.sprite = face;
        this.spriteRender_Tile.sprite = tile;
        this.GridPosX = gridPosX;
        this.GridPosY = gridPosY;
        this.grid = grid;
        this.spriteRender_Selected.gameObject.SetActive(false);
    }



    public void OnMouseDown()
    {

        
        
         //   Debug.Log("Selected Card ID: " + this.id + " x=" + this.CardGrid.x + " y=" + this.CardGrid.y);
            EventHandler.i.Select(this,this,null);

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
            spriteRender_Selected.gameObject.SetActive(true);

            //--sety sort
            spriteRender_Face.sortingOrder += 10;
            spriteRender_Tile.sortingOrder += 10;
            spriteRender_Selected.sortingOrder += 10;


            this.transform.DOScale(new Vector3(1.1f, 1.1f, 0), 0.2f).SetEase(Ease.InOutFlash);
        }
        else {
            spriteRender_Selected.gameObject.SetActive(false);
            //--sety sort
            spriteRender_Face.sortingOrder -= 10;
            spriteRender_Tile.sortingOrder -= 10;
            spriteRender_Selected.sortingOrder -= 10;
            this.transform.DOScale(new Vector3(1f, 1f, 0), 0.2f).SetEase(Ease.InOutFlash);
        }
    }


    

    public void DestroyCard() {
        Sequence s = DOTween.Sequence();
        s.Append(this.transform.DOScale(Vector3.one * 0.8f, 0.2f)).Append(this.transform.DOScale(Vector3.zero, 0.2f)).Join(GetComponent<SpriteRenderer>().DOFade(0, 0.2f));
        s.OnComplete(FinalDestroy);
    }

    public void FinalDestroy() {

        this.grid.GetGridObject(GridPosX, GridPosY).isNotEmpty = false;
        this.transform.DOKill(true);
        
        Destroy(this.gameObject);
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
        else
        {
            isNotEmpty = false;
        }

    }

    public void SetValue(bool isAvaliable)
    {
               isNotEmpty = isAvaliable;
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