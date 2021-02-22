using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;

public class PopUpScript : MonoBehaviour
{
    [SerializeField] private TMP_Text Message;
    [SerializeField] private Button Ok;
    [SerializeField] private Button Exit;
    [SerializeField] private CanvasGroup PopUpContent;
    [SerializeField] private CanvasGroup Overlay;





    public Button GetButton() {

        return Ok;
    }


    public void Init(string message, Action callbackOK, Action callbackExit) {
        Message.text = message;
        Ok.onClick.AddListener(callbackOK.Invoke);
        Ok.onClick.AddListener(() => OnHide());
        Exit.onClick.AddListener(callbackExit.Invoke);
        Exit.onClick.AddListener(() => OnHide());

        OnShow();

    }


    

    public void OnShow() {
        Sequence s = DOTween.Sequence();
        s.Append(Overlay.DOFade(1, .3f).From(0f))
            .Append(PopUpContent.DOFade(1, 1f).From(0));
        
    }


    public void OnHide() {
        Sequence s = DOTween.Sequence();
        s.Append(PopUpContent.DOFade(0, .3f).From(1))
            .Join(Overlay.DOFade(0, .3f).From(1));

        s.OnComplete(() => Destroy(this.gameObject));

    }

    private void OnEnable()
    {
        PopUpContent.alpha = 0;
        Overlay.alpha = 0;
    }

}
