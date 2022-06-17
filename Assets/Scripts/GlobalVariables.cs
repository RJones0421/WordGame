using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static Word word;
    public static PlayerController player;

    private void Awake()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}
