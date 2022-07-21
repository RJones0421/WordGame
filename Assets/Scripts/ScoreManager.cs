using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    public GameObject scoreHolder;
    private TMP_Text scoreText;

    public GameObject highScoreHolder;
    private TMP_Text highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = scoreHolder.GetComponent<TMP_Text>();
        highScoreText = highScoreHolder.GetComponent<TMP_Text>();

        highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
    }

    public void AddScore(int score)
    {
        Int32.TryParse(scoreText.text, out int currentScore);
        int totalScore = currentScore + score;
        scoreText.text = (currentScore + score).ToString();

        if (totalScore > PlayerPrefs.GetInt("highscore"))
        {
            highScoreText.text = "Highscore: " + totalScore.ToString();
        }
    }
}
