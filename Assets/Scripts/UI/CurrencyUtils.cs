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
    public static string item_1_key = "1";
    public static string item_2_key = "2";
    public static string item_3_key = "3";
    public static string item_4_key = "4";
    public static string item_5_key = "5";
    public static string item_6_key = "6";


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
       // get int defaults to 0, if the key does not exist
        int current_currency_amount = PlayerPrefs.GetInt(currency_amount_keyname);
        int new_currency_amount = current_currency_amount + finalScore;
        PlayerPrefs.SetInt(currency_amount_keyname,new_currency_amount);

        int amount = PlayerPrefs.GetInt(currency_amount_keyname);
        Debug.Log("this is your current currency amount " + amount);
    }


    public static void removeCurrency(string item_name)
    {
        try
        {
            if(items_value.Count == 0){
                populateCostMap();
            }
            // haven't converted the item to cost yet
            // int cost = Convert.ToInt32(item_name);
            int cost = items_value[item_name];
            // used when buying a power up in the shop, deducts the cost from your current balance
            int currency_balance = PlayerPrefs.GetInt(currency_amount_keyname);

            if (currency_balance >= cost) {
                int new_currency_balance = currency_balance - cost;
                PlayerPrefs.SetInt(currency_amount_keyname,new_currency_balance);

                // updating players inventory for quantity of item purchased
                int item_num = Convert.ToInt32(item_name);
                int item_quantity = PlayerPrefs.GetInt(item_name);
                int new_item_quantity = item_quantity + 1;
                PlayerPrefs.SetInt(item_name, new_item_quantity);
            }

            int new_balance = PlayerPrefs.GetInt(currency_amount_keyname);



        }
        catch (FormatException e)
        {
            Debug.Log("remove  Currency error: " + e.Message);
        }

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
        try
        {
            int currency_balance = PlayerPrefs.GetInt(currency_amount_keyname);
            GameObject inputFieldGo = GameObject.Find(gameObject_name);
            TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
            // the text that is displayed on screen
            inputFieldCo.text = "Current Balance " + currency_balance.ToString();
        }
        catch (System.Exception e)
        {
            Debug.Log("displayCurrency error: " + e.ToString());
        }
    }

    public static void displayQuantity(string item_num)
    {
        try
        {
            // Text_Item1_Quantity
            int item_quantity = PlayerPrefs.GetInt(item_num);
            string gameObject_name = "Text_Item" + item_num + "_Quantity";
            GameObject inputFieldGo = GameObject.Find(gameObject_name);
            TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
            // the text that is displayed on screen
            inputFieldCo.text = "Quantity: " + item_quantity.ToString();
        }
        catch (System.Exception e)
        {
            Debug.Log("displayQuantity error: " + e.ToString());
        }
    }

    public static void resetCurrencyBalance() {
        PlayerPrefs.DeleteKey(currency_amount_keyname);
        PlayerPrefs.SetInt(currency_amount_keyname,0);
    }

    // returns true or false depending on if player has enough shop items to use
    // item num should be a string 1-6
    public static bool useShopItem(string item_num)
    {
        int item_quantity = PlayerPrefs.GetInt(item_num);
        if (item_quantity > 0)
        {
            item_quantity -= 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}