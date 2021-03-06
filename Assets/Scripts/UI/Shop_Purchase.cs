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
        Debug.Log("START OF SHOP_PURCHASE");
        displayCurrency();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("UPDATE OF SHOP_PURCHASE");
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
        int pointsAvailable = PlayerPrefs.GetInt(CurrencyUtils.currency_amount_keyname, 0);
        Debug.Log("curreny amount display in shop opening: " + pointsAvailable);
        inputFieldCo.text = "Points Available: " + pointsAvailable;

        CurrencyUtils.displayQuantity("1");
        CurrencyUtils.displayQuantity("2");
        CurrencyUtils.displayQuantity("3");
        CurrencyUtils.displayQuantity("4");
    }

    public static void activatePowerUpUI(String powerupName) {
        Color color = new Color(255, 223, 0);
        activatePowerUpUIWithColor(powerupName,color);

        if (powerupName == "ScoreMultiplier")
        {
            foreach (GameObject box in GameObject.FindGameObjectsWithTag("WordBox"))
            {
                box.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 0.0f);
            }
        }
    }

    public static void deactivatePowerUpUI(String powerupName) {
        Color color = new Color(242, 140, 128);
        activatePowerUpUIWithColor(powerupName,color);

        if (powerupName == "ScoreMultiplier")
        {
            foreach (GameObject box in GameObject.FindGameObjectsWithTag("WordBox"))
            {
                box.GetComponent<SpriteRenderer>().color = new Color(195.0f, 195.0f, 195.0f);
            }
        }
    }

    public static void activatePowerUpUIWithColor(String powerupName, Color newColor) {
        GameObject inputFieldGo = GameObject.Find("Button_" + powerupName);
        Button inputFieldCo2 = inputFieldGo.GetComponent<Button>();
        ColorBlock  colorBlockHuman = inputFieldCo2.colors;
        colorBlockHuman.normalColor = newColor;
        inputFieldCo2.colors = colorBlockHuman;
    }

    public static void resetShopPowerUpUI() {
        Color color = new Color(242, 140, 128);
        activatePowerUpUIWithColor("ScoreMultiplier",color);
        activatePowerUpUIWithColor("ExtraLife",color);
        activatePowerUpUIWithColor("Anagram",color);
        activatePowerUpUIWithColor("PauseTime",color);
    }


}
