using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuggestedWord : MonoBehaviour
{
    [SerializeField] private StreamSpawning streamSpawning;
    [SerializeField] private TMP_Text suggestedWord;
    [SerializeField] private PlayerController player;

    private void Update()
    {
        if (player.transform.position.y >= GlobalVariables.yPosChange)
        {
            suggestedWord.text = streamSpawning.currentWord.ToUpper();
        }
    }
}
