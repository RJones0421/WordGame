using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SuggestedWord : MonoBehaviour
{
    [SerializeField] private StreamSpawning streamSpawning;
    [SerializeField] private TMP_Text suggestedWord;

    private void Update()
    {
        suggestedWord.text = streamSpawning.currentWord;
    }
}
