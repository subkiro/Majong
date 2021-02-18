using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Mahjong/CardObj")]
public class CardSO : ScriptableObject
{
    
    public Sprite face;
    public Sprite tile;
    public int id;
    public GameObject CardPrefab;
    public bool isSelected = false;

    public GameObject CreateCard(GenericGrid<CardGrid> grid,int x, int y) {
        GameObject tmp = Instantiate(CardPrefab);
        tmp.GetComponent<Card>().InitCard(id, face, tile,grid, x,  y); 

        return tmp;
    }

}

