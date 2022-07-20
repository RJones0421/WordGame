using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System;

public class Shop_UI_Object2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START OF SHOP_PURCHASE");
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void displayCurrency_onAppear(){
        GameObject inputFieldGo = GameObject.Find("Text_Shop_Currency");
        TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        int pointsAvailable = PlayerPrefs.GetInt(CurrencyUtils.currency_amount_keyname, 0);
        Debug.Log("curreny amount display in shop opening: " + pointsAvailable);
        inputFieldCo.text = "Points Available: " + pointsAvailable;

        CurrencyUtils.displayQuantity("1");
        CurrencyUtils.displayQuantity("2");
        CurrencyUtils.displayQuantity("3");
        CurrencyUtils.displayQuantity("4");
    }



}
