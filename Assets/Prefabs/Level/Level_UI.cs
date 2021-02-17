using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Level_UI : MonoBehaviour
{
    [SerializeField] Image LevelImage;
    [SerializeField] TMP_Text LevelNumber;
    [SerializeField] Image Star;
    [SerializeField] Button button;
    private LevelSO levelSriptable;

    public void Init(LevelSO level)
    {
       this.levelSriptable = level;
       this.LevelNumber.text = level.levelID.ToString();
       this.LevelImage.sprite = level.LevelPhoto;
       Star.enabled = (level.isCompleted) ? true : false;

       button.onClick.AddListener(()=>{ GameManager.i.LoadCurrentLevel(this.levelSriptable); });
    }
}
