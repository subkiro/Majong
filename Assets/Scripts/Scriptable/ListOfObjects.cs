using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Mahjong/ListObjects")]
public class ListOfObjects : ScriptableObject
{
    public List<LevelSO> AllLevels;
    public List<CardSO> AllCards;



}
