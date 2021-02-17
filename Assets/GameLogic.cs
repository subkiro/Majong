using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameLogic : MonoBehaviour
{
    private int x = 5;
    private int y = 5;
    private int cardSize = 1; 
    GenericGrid<CardGrid> grid;

    private Card selecedCard;
    




    private void Start()
    {
        CreateGrid();
        FillCards();
    }


    public void CreateGrid() {
        
        x = GameManager.i.CurrentLevel.x;
        y = GameManager.i.CurrentLevel.y;
        grid = new GenericGrid<CardGrid>(x,y,cardSize, new Vector3(-x * cardSize / 2- 0.5f, -y * cardSize / 2 -0.5f, 0), (GenericGrid<CardGrid> g, int x, int y) => new CardGrid(g, x, y));
    }
    
    public void FillCards() {

        Debug.Log(grid.width);
        Debug.Log(grid.height);
        char[,] lvl = GameManager.i.CurrentLevel.GetLevelArray();
        List<CardSO> cardList = GameManager.i.listOfAssets.AllCards;
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                //Test
                if (lvl[x, y] == '0')
                {

                    CardSO random = cardList[UnityEngine.Random.Range(0, cardList.Count-1)];
                    GameObject card = random.CreateCard(grid.GetGridObject(x, y));

                    card.transform.parent = this.transform;
                    card.transform.position = grid.GetWorldPosition(x, y);
                    grid.GetGridObject(x, y).SetValue(card.GetComponent<Card>());
                }
                else {

                    grid.GetGridObject(x, y).SetValue(null);
                }
            }
        }
    
    }


    public void SelectCard(object sender,Card c) {
        if (selecedCard == null)
        {

            selecedCard = c;
        }
        else if (selecedCard != null) {
            if (grid.GetGridObject(c.transform.position).isTheSame(selecedCard))
            {
                c.isSelected = !c.isSelected; //disselect
                Debug.Log("Is the Same Card");
                selecedCard = null;
            }
            else if (c.id == selecedCard.id) {

                //Check if is possible to match

                if (CheckPath(c, selecedCard)) {
                    SuccessMatchGivePoints(50,c,selecedCard);
                }

                //else make penalty

            }
        
        
        }
    }



    public void SuccessMatchGivePoints(int points,Card c1,Card c2) {

        //give points
        //set grid points to  Empty...

        Debug.Log("OOOOOOOOOKI");
        Destroy(c1.gameObject);
        Destroy(c2.gameObject);

        if (this.transform.childCount == 0) {
            GameManager.i.CurrentLevel.isCompleted = true;
            SceneManager.LoadScene(0);
       }
    }


    private bool CheckPath(Card c1, Card c2)
    {
        return true; //for testing :)
    }

    private void OnEnable()
    {
        EventHandler.i.SelectAction += SelectCard;
    }
    private void OnDisable()
    {
        EventHandler.i.SelectAction -= SelectCard;
    }
}
