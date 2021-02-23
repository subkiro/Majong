using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;

public class ScoreSystem : MonoBehaviour
{

    public int totalScore;
    public int currentScore;

    [SerializeField] TMP_Text CurrentScoreText;
    [SerializeField] TMP_Text TotalScoreText;

    public  static ScoreSystem instance;
    void Awake() => instance =this;

    private void Start()
    {
       
        currentScore = 0;

        totalScore = GameManager.i.CurrentLevel.MaxLevelScore;
       // totalScore = PlayerPrefs.GetInt("TotalScore", totalScore);
        TotalScoreText.SetText(totalScore.ToString());
        
     

    }

  
    public void AddPoints(int Points) {

       currentScore +=  Points;
       CurrentScoreText.SetText(currentScore.ToString());
       CurrentScoreText.rectTransform.DOLocalJump(Vector3.zero, 2,1,0.3f).SetRelative();
        SoundManager.instance.PlayVFX("Buble");
        if (currentScore > totalScore) {
            totalScore = currentScore;
            TotalScoreText.SetText(totalScore.ToString());
            GameManager.i.CurrentLevel.MaxLevelScore = totalScore;
           // PlayerPrefs.SetInt("TotalScore", totalScore);
        }


        


     
    }

    public void SubtractPoints(int Points) {
        SoundManager.instance.PlayVFX("negative");
        currentScore -= Points;
        currentScore = (currentScore<0)? 0:currentScore;
        CurrentScoreText.SetText(currentScore.ToString());
        Color color = Camera.main.backgroundColor;
        
        Camera.main.DOColor(color, 0.5f).From(Color.black);
    }
    
    
   

    public int GetTotalScore() { 
        return totalScore;
    }

    public void ResetCurrentScore() {
        currentScore = 0;
    }

    public void SpawnText(Transform trans, Vector3 pos, string text,Color color) {
        TextMesh t =  Subkiro.CreateTextMesh(text, trans.parent, pos,100,0.1f);
        t.gameObject.AddComponent<SortingGroup>().sortingOrder = 100;
        t.gameObject.transform.position = pos;
        t.color = color;
        DOTween.ToAlpha(() => color, x => t.color = x, 0, 1f);
        t.transform.DOMoveY(2, 1f).SetRelative();
        
        
        Destroy(t.gameObject, 1.1f);
    }

}
