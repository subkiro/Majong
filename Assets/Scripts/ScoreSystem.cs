﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
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

        if (currentScore > totalScore) {
            totalScore = currentScore;
            TotalScoreText.SetText(totalScore.ToString());
            GameManager.i.CurrentLevel.MaxLevelScore = totalScore;
           // PlayerPrefs.SetInt("TotalScore", totalScore);
        }

     
    }

    public void SubtractPoints(int Points) {
        currentScore -= Points;
        currentScore = (currentScore<0)? 0:currentScore;
        CurrentScoreText.SetText(currentScore.ToString());
        Color color = Camera.main.backgroundColor;
        
        Camera.main.DOColor(color, 0.5f).From(Color.red);
    }
    
    
   

    public int GetTotalScore() { 
        return totalScore;
    }

    public void ResetCurrentScore() {
        currentScore = 0;
    }

}