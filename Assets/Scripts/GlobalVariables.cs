using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static Word word;
    public static PlayerController player;
    public Sprite blank;

    public static float yPosChange = 0f;
    public static bool updateWordChangeHeight;

    private void Awake()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
}
