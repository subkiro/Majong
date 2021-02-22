using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using DG.Tweening;
using UnityEngine.UI;
public class GameLogic : MonoBehaviour
{
    private int x = 5;
    private int y = 5;
    private int cardSize = 1; 
    GenericGrid<CardGrid> grid;

    private Card selecedCard;
    List<CardGrid> cardGridList; // this is temp card list
    private float lastTimeClicked = 0;




    private void Start()
    {
        CreateGrid();
        cardGridList = DefineGridCardPositionsOfLevel(GameManager.i.CurrentLevel);
        FillRandomly(cardGridList);
        ButtonListenersInit();





    }
    private void ButtonListenersInit() {
       
        GameObject.FindGameObjectWithTag("HintButton")?.GetComponent<Button>().onClick.AddListener(()=>FindeWithBruteForce());
    }
    private  void FillRandomly(List<CardGrid> cardGridList)
    {
        int[] test = new int[cardGridList.Count];
       


        List<CardSO> cardList = GameManager.i.listOfAssets.AllCards;
        List<CardSO> tweenCards = new List<CardSO>();

        for (int i = 0; i < test.Length; i++)
        {
            test[i] = i;
           
        }
       


        int[] testRand = Subkiro.GetRandomArray(test.Length, test.Length);
        
        for (int i = 0; i < test.Length/2; i++)
        {
            CardSO random = cardList[Random.Range(0, cardList.Count-1)];
            tweenCards.Add(random);
            tweenCards.Add(random);
        }
        //Debug.Log("TweenCount -->" + tweenCards.Count);
       // Debug.Log("CardlistCount -->" + cardGridList.Count);
       // Debug.Log("int Aerray with randoms -->" + test.Length);
        for (int i = 0; i < test.Length; i++)
        {
            int x = cardGridList[testRand[i]].x;
            int y = cardGridList[testRand[i]].y;

            GameObject card = tweenCards[i].CreateCard(grid,x,y);
            card.transform.parent = this.transform;
            card.transform.position = grid.GetWorldPosition(x, y) + new Vector3(cardSize, cardSize) * .5f;
            grid.GetGridObject(x, y).SetValue(card.GetComponent<Card>());
            

        }


    }



    private List<CardGrid> DefineGridCardPositionsOfLevel(LevelSO currentLevel)
    {
      
        char[,] lvl = GameManager.i.CurrentLevel.GetLevelArray();
        List<CardGrid> avaliblePositions = new List<CardGrid>();
        for (int x = 0; x < grid.width; x++)
        {
            for (int y = 0; y < grid.height; y++)
            {
                //Test
                if (lvl[x, y] == 'X')
                {

                    grid.GetGridObject(x, y).SetValue(true);
                    avaliblePositions.Add(grid.GetGridObject(x, y));
                }
                else
                {

                    grid.GetGridObject(x, y).SetValue(false);
                }
            }
        }

        //calculate avaliable positions


     
        return avaliblePositions;

    }




    public void CreateGrid() {
        
        x = GameManager.i.CurrentLevel.GetLevelArray().GetLength(0);
        y = GameManager.i.CurrentLevel.GetLevelArray().GetLength(1);
        grid = new GenericGrid<CardGrid>(x,y,1f,1f, new Vector3(-x  / 2, -y / 2, 0), (GenericGrid<CardGrid> g, int x, int y) => new CardGrid(g, x, y));
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
                if (lvl[x, y] == 'X')
                {

                    CardSO random = cardList[UnityEngine.Random.Range(0, cardList.Count-1)];
                    GameObject card = random.CreateCard(grid,x,y);

                    card.transform.parent = this.transform;
                    card.transform.position = grid.GetWorldPosition(x, y)+ new Vector3(cardSize, cardSize) * .5f;
                    grid.GetGridObject(x, y).SetValue(card.GetComponent<Card>());
                }
                else {

                    grid.GetGridObject(x, y).SetValue(null);
                }
            }
        }
    
    }


    public void SelectCard(object sender, Card c,Card cExtra)
    {
        if (cExtra != null) {
            selecedCard = cExtra;
        }

        if (!CheckIfCanSearch(c.GridPosX, c.GridPosY)) {

            if (selecedCard != null)
            {
                selecedCard.IsSelected = false;
                selecedCard = null;
            }


                return;
        }
        

        if (selecedCard == null)
        {
            //Debug.Log("First Card");
            selecedCard = c;
            selecedCard.IsSelected = true;
        }
        else
        {
            c.IsSelected = true;

            if (grid.GetGridObject(c.transform.position).isTheSame(selecedCard))
            {
                
                c.IsSelected = false; //disselect
               // Debug.Log("Same Card");
                selecedCard = null;
            }
            else if (c.id == selecedCard.id && !grid.GetGridObject(c.transform.position).isTheSame(selecedCard))
            {
                //Check if is possible to match
                 


                if (CheckPath(c, selecedCard))
                {
                    SuccessMatchGivePoints(15, c, selecedCard);
                }
                else
                {
                    ScoreSystem.instance.SubtractPoints(10); //sub points to the user
                    c.IsSelected = false;
                    selecedCard.IsSelected = false;
                    selecedCard = null;
                }



            }
            else
            {
                //else make penalty
               // Debug.Log("Direrent ID");
                c.IsSelected = false;
                selecedCard.IsSelected = false;
                selecedCard = null;
                ScoreSystem.instance.SubtractPoints(10); //sub points to the user
            }


        }


    }

    public bool CheckIfCanSearch(int x, int y) {

        if (x > 0 && x < grid.width-1 && y > 0 && y<grid.height-1) {
        //    Debug.Log("x = " + x+ "--------"+"y= "+y);
            if (grid.GetGridObject(x - 1, y).isNotEmpty &&
                grid.GetGridObject(x + 1, y).isNotEmpty &&
                 grid.GetGridObject(x, y - 1).isNotEmpty &&
                  grid.GetGridObject(x, y + 1).isNotEmpty) {
                return false;
            }
        }

        return true;
    }


    public void SuccessMatchGivePoints(int points,Card c1,Card c2) {


        c1.DestroyCard(); 
        c2.DestroyCard();

       
        ScoreSystem.instance.AddPoints(15); //Give points to the user

        if (this.transform.childCount <=2) {
            string score = "Score " + ScoreSystem.instance.currentScore + " - Max Score " + ScoreSystem.instance.totalScore;
            UIManager.instance.ShopPopUpWin(score, () => SceneManager.LoadScene(0));
            GameManager.i.CurrentLevel.isCompleted = true;
            SoundManager.instance.PlayVFX("positive1");


        }
    }



    public void FindeWithBruteForce()
    {
               
        for (int i = 0; i < cardGridList.Count; i++)
        {
            for (int j = i + 1; j < cardGridList.Count; j++)
            {
                if (CheckIfCanSearch(cardGridList[j].card.GridPosX, cardGridList[j].card.GridPosY) && CheckPath(cardGridList[i].card, cardGridList[j].card) && cardGridList[i].card.id == cardGridList[j].card.id) {
                    Debug.Log("MatchFound--->" + "Card1: =" + cardGridList[i].card.id + " and Card2: = " + cardGridList[j].card);
                    cardGridList[i].card?.transform.DOPunchScale(Vector3.one * 0.5f, .5f);
                    cardGridList[j].card?.transform.DOPunchScale(Vector3.one * 0.5f, .5f);
                    SoundManager.instance.PlayVFX("ok1");
                    return ;
                } 

            }
        }
      
    }
    private bool CheckPath(Card c2, Card c1)
    {

        
        int startX =(int)c1.GetGridPos().x;
        int startY =(int)c1.GetGridPos().y;

       // Debug.Log("Recurse Started ");

                int min = 99999;


     


        int temp = DFS(startX+1, startY, Directions.Left, 0, c2);

     
        if (temp < min && temp>=0) 
        {
            min = temp;

            if (min <= 2 && min >= 0)
            {
             
                return true;
            }

        }

        


        temp = DFS(startX - 1, startY, Directions.Right, 0, c2);
      
        if (temp < min && temp >=0)
        {
            min = temp;

            if (min <= 2 && min >= 0)
            {
                
                return true;
            }
        }

                temp = DFS(startX, startY + 1, Directions.Up, 0, c2);
        
        if (temp < min && temp >= 0)
        {
            min = temp;

            if (min <= 2 && min >= 0)
            {
              
                return true;
            }
        }

                temp = DFS(startX, startY - 1, Directions.Down, 0, c2);
      
        if (temp < min && temp >=0) 
        { 
            min = temp;
            if (min <= 2 && min >= 0)
            {
               
                return true;
            }
        }


       
        return false;
             
        
    }

    private int DFS(int curX, int curY, Directions source, int score, Card endCard)
    {
       
        /*
         * TERMINAL CONDITIONS
         * */
        //If out of bounds +1 return -1

        if (score > 3) return score;

      //  Debug.Log("curX =" + curX + "and curY=" + curY);

        if ((curX >= -1 && curX < grid.width+1) && (curY >= -1 && curY < grid.height+1)) {

           


            if ((curX >= 0 && curX < grid.width ) && (curY  >= 0 && curY < grid.height))
            {
                if (grid.GetGridObject(curX, curY).isNotEmpty)
                {

                    // if (grid.GetGridObject(curX, curY).card.id != endCard.id)
                    if (curX==endCard.GridPosX && curY==endCard.GridPosY)
                    {

                        return score;

                    }
                    else 
                        {
                        //  Debug.Log("Fiiiiiiiiiiid score = " + score);

                        return -1;
                    }

                }
               
            }
            
        }
        else
        {
           
            return -1;
        }
        //if in bounds
            //If x,y contains card AND not end card return -1 because obstacle
            //IF x,y contains card AND is end card return score


        /*
         * RECURSIVE CALLS
         * */
        int min = 9999;
        if(source == Directions.Left)
        {
            int temp = DFS(curX + 1, curY, Directions.Left, score, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX, curY + 1, Directions.Up, score+1, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX, curY - 1, Directions.Down, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;

            if (min != 9999) score = min;
            else score = -1;
        }else if (source == Directions.Right)
        {
            int temp = DFS(curX - 1, curY, Directions.Right, score, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX, curY + 1, Directions.Up, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX, curY - 1, Directions.Down, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;

            if (min != 9999) score = min;
            else score = -1;
        }
        else if (source == Directions.Up)
        {
            int temp = DFS(curX , curY+1, Directions.Up, score, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX-1, curY, Directions.Right, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX+1, curY, Directions.Left, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;

            if (min != 9999) score = min;
            else score = -1;
        }
        else if (source == Directions.Down)
        {
            int temp = DFS(curX, curY - 1, Directions.Down, score, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX - 1, curY, Directions.Right, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;
            temp = DFS(curX + 1, curY, Directions.Left, score + 1, endCard);
            if (temp >= 0 && temp < min) min = temp;

            if (min != 9999) score = min;
            else score = -1;
        }

        /*
         * RECURSIVE RETURN
         * */
        return score;
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

public enum Directions
{
    Left, Right, Up, Down
}
