using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreHolder;
    private TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreHolder.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        Int32.TryParse(scoreText.text, out int currentScore);
        scoreText.text = (currentScore + score).ToString();
    }
}
