using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CurrencyUtils : MonoBehaviour
{
    static string currency_amount_keyname = "currency";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void addCurrency(int finalScore)
    {
       // currency udpate = converts final score to in game currency
       // increases your currency balance
        int current_currency_amount = PlayerPrefs.GetInt(currency_amount_keyname);
        int new_currency_amount = current_currency_amount + finalScore;
        PlayerPrefs.SetInt(currency_amount_keyname,new_currency_amount);

        int amount = PlayerPrefs.GetInt(currency_amount_keyname);
        Debug.Log("this is your current currency amount " + amount);
    }


    public static void removeCurrency(int cost)
    {
       // used when buying a power up in the shop, deducts the cost from your current balance
        int currency_balance = PlayerPrefs.GetInt(currency_amount_keyname);
        int new_currency_balance = currency_balance - cost;
        PlayerPrefs.SetInt(currency_amount_keyname,new_currency_balance);

        int amount = PlayerPrefs.GetInt(currency_amount_keyname);
        Debug.Log("this is your new current currency amount " + amount);
    }
}