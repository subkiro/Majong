using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Transform LevelContainer;
    [SerializeField] private GameObject LevelPrefab;


    private void Start()
    {
        OnShow();
    }

    public void OnShow() {

        SoundManager.instance.PlayVFX("positive2");
        List<LevelSO> levels = GameManager.i.listOfAssets.AllLevels;
        foreach (LevelSO item in levels)
        {
           GameObject lvl =  Instantiate(LevelPrefab, LevelContainer);
           lvl.GetComponent<Level_UI>().Init(item);     
        }

    }

    
}
