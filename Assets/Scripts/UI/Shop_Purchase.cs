using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class Shop_Purchase : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickButton()
    {
        
        Debug.Log("Inside onclickButton()");
        var go = EventSystem.current.currentSelectedGameObject;
        
        
        if (go != null){
            Debug.Log("Clicked on : " + go.name);
            GameObject inputFieldGo =  GameObject.Find("Button_ShopItem2_Text");
            TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
            inputFieldCo.text = go.name;
        }
         else{
            
            GameObject inputFieldGo =  GameObject.Find("Button_ShopItem2_Text");
            TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
            inputFieldCo.text = "Random Button Clicked";
         }

           
    }
}
