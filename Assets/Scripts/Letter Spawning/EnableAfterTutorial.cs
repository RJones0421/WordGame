using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnableAfterTutorial : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private string tutorialWord;
    [SerializeField] private TMP_Text text;
    [SerializeField] private SuggestedWord suggested;

    private bool firstFrame;

    private void Update()
    {
        text.text = tutorialWord;
        if (player.transform.position.y > transform.position.y) EnableSuggestedWord();
    }

    private void EnableSuggestedWord()
    {
        suggested.enabled = true;
        Destroy(this);
    }
}