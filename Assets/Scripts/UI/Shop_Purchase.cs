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
    }

    public static void actiatePowerUpUI(String powerupName) {
        Color color = new Color(0, 128, 0);
        actiatePowerUpUIWithColor(powerupName,color);
    }

    public static void actiatePowerUpUIWithColor(String powerupName, Color newColor) {
        GameObject inputFieldGo = GameObject.Find("Button_" + powerupName);
        Button inputFieldCo2 = inputFieldGo.GetComponent<Button>();
        ColorBlock  colorBlockHuman = inputFieldCo2.colors;
        colorBlockHuman.normalColor = newColor;
        inputFieldCo2.colors = colorBlockHuman;
    }

    public static void resetShopPowerUpUI() {
        Color color = new Color(242, 140, 128);
        actiatePowerUpUIWithColor("ScoreMultiplier",color);
        actiatePowerUpUIWithColor("ExtraLife",color);
        actiatePowerUpUIWithColor("Anagram",color);
        actiatePowerUpUIWithColor("PauseTime",color);
    }


}
