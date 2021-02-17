using System;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public static EventHandler i;


    //ActionEvents
    public Action<object,Card> SelectAction;

    private void Awake()
    {
        i = this;
    }



    public void Select(object sender,Card c)
    {
        
        SelectAction?.Invoke(sender,c);
       
    }
}
