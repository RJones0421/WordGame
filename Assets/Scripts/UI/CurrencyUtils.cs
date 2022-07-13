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


    // average word submitted score is about 235, currency awarded will be 2
    public static void populateCostMap(){
        items_value.Add("1", 5);
        items_value.Add("2", 15);
        items_value.Add("3", 7);
        items_value.Add("4", 10);
    }

    public static void addCurrency(int finalScore)
    {
       // currency udpate = converts final score to in game currency (1 to 1)
       // increases your currency balance
       // get int defaults to 0, if the key does not exist
       int temp_final_currency_amount = finalScore / 100;
        finalScore = finalScore / 100;
        Debug.Log("final final currency amount " + finalScore);
        Debug.Log("final currency amount " + temp_final_currency_amount);


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

            int amount = PlayerPrefs.GetInt(currency_amount_keyname);

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

    public static void displayQuantityDynamic(string item_num, string gameObject_name, string display_text)
    {
        try
        {
            // Text_Item1_Quantity
            int item_quantity = PlayerPrefs.GetInt(item_num);
            GameObject inputFieldGo = GameObject.Find(gameObject_name);
            TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
            // the text that is displayed on screen
            inputFieldCo.text = display_text  + item_quantity.ToString();
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
        item_quantity = 11;
        Debug.Log("player has x items " + item_quantity);
        if (item_quantity > 0)
        {
            item_quantity -= 1;
            PlayerPrefs.SetInt(item_num, item_quantity);

            // change the visablity setting for the corresponding item icon
            string icon_name = item_num + "_icon";
            if (item_quantity == 0)
            {
                // the string here should be the name of the item's icon on line 160
                GameObject inputFieldGo = GameObject.Find(icon_name);
                inputFieldGo.SetActive(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void showShopItemIcon(string item_num)
    {
        int item_quantity = PlayerPrefs.GetInt(item_num);
        string icon_name = item_num + "_icon";


        if (item_quantity == 0)
        {
            // the string here should be the name of the item's icon on line 160
            GameObject inputFieldGo = GameObject.Find(icon_name);
            inputFieldGo.SetActive(false);
        }
        else
        {
            GameObject inputFieldGo = GameObject.Find(icon_name);
            inputFieldGo.SetActive(true);
        }
    }
}