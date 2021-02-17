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

    public GameObject CreateCard(CardGrid c) {
        GameObject tmp = Instantiate(CardPrefab);
        tmp.GetComponent<Card>().InitCard(id, face, tile,c); 

        return tmp;
    }

}

