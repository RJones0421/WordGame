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
        var itemSelected = (go.name)[(go.name).Length-1];
        CurrencyUtils.removeCurrency(itemSelected.ToString());
        CurrencyUtils.displayQuantity(itemSelected.ToString());
        displayCurrency();
    }

    public void displayCurrency(){
        GameObject inputFieldGo = GameObject.Find("Text_Shop_Currency");
        TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        int pointsAvailable = PlayerPrefs.GetInt(CurrencyUtils.currency_amount_keyname);

        inputFieldCo.text = "Points Available: " + pointsAvailable;

        CurrencyUtils.displayQuantity("1");
        CurrencyUtils.displayQuantity("2");
        CurrencyUtils.displayQuantity("3");
        CurrencyUtils.displayQuantity("4");
        CurrencyUtils.displayQuantity("5");
        CurrencyUtils.displayQuantity("6");
    }

}
