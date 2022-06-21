using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System;

public class CurrencyUtils : MonoBehaviour
{
    public static string currency_amount_keyname = "currency";
    static Dictionary<string, int> items_value = new Dictionary<string, int>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void populateCostMap(){
        items_value.Add("1", 1);
        items_value.Add("2", 2);
        items_value.Add("3", 3);
        items_value.Add("4", 4);
        items_value.Add("5", 5);
        items_value.Add("6", 6);
    }

    public static void addCurrency(int finalScore)
    {
       // currency udpate = converts final score to in game currency (1 to 1)
       // increases your currency balance
        int current_currency_amount = PlayerPrefs.GetInt(currency_amount_keyname);
        int new_currency_amount = current_currency_amount + finalScore;
        PlayerPrefs.SetInt(currency_amount_keyname,new_currency_amount);

        int amount = PlayerPrefs.GetInt(currency_amount_keyname);
        Debug.Log("this is your current currency amount " + amount);
    }


    public static void removeCurrency(string item_name)
    {
       // used when buying a power up in the shop, deducts the cost from your current balance
        int currency_balance = PlayerPrefs.GetInt(currency_amount_keyname);

        if(items_value.Count == 0){
            populateCostMap();
        }
        int cost = items_value[item_name];
        int new_currency_balance = currency_balance - cost;
        PlayerPrefs.SetInt(currency_amount_keyname,new_currency_balance);
        int amount = PlayerPrefs.GetInt(currency_amount_keyname);
        Debug.Log("this is your new current currency amount " + amount);
    }

    // returns the current currency balance
    public static int getBalance()
    {
        int currency_balance = PlayerPrefs.GetInt(currency_amount_keyname);
        return currency_balance;
    }

    // grabs any game object based on name (gameObject_name) and changes the text
    // inside of it
    // possible use case: currency display in shop
    public static void displayCurrency(string gameObject_name)
    {
        int currency_balance = PlayerPrefs.GetInt(currency_amount_keyname);
        GameObject inputFieldGo = GameObject.Find(gameObject_name);
        TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        // the text that is displayed on screen
        inputFieldCo.text = "Current Balance " + currency_balance.ToString();
    }

    public static void resetCurrencyBalance() {
        PlayerPrefs.DeleteKey(currency_amount_keyname);
        PlayerPrefs.SetInt(currency_amount_keyname,0);
    }
}