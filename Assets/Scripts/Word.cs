using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Word : MonoBehaviour
{

    [SerializeField] private WordEvaluator evaluator;

    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    [SerializeField] private SpriteRenderer leftSidebar;
    [SerializeField] private SpriteRenderer rightSidebar;

    private List<LetterClass> letters = new List<LetterClass>();

    private int currentLetterBox = 0;


    public GameObject timer;

    private Timer timerClass;
    
    private string word = "";

    public GameObject scoreHolder;
    private TMP_Text scoreText;

    private void Awake()
    {
        timerClass = timer.GetComponent<Timer>();
        scoreText = scoreHolder.GetComponent<TMP_Text>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) submitWord();
    }

    public void SetSidebars(List<GameObject> walls)
    {
        leftSidebar = walls[0].GetComponent<SpriteRenderer>();
        rightSidebar = walls[1].GetComponent<SpriteRenderer>();

        leftSidebar.color = Color.gray;
        rightSidebar.color = Color.gray;
    }

    public bool addLetter(LetterClass newLetter)
    {
        if (newLetter.Letter == '_') return false;
        if (letters.Count >= 8) return false;

        else
        {
            letters.Add(newLetter);
            word += newLetter.Letter;
            sprites[currentLetterBox].sprite = newLetter.LetterSprite;
            currentLetterBox++;

            // Update Sidebars
            bool valid = evaluator.IsValidWord(word);
            if (valid) {
                leftSidebar.color = Color.green;
                rightSidebar.color = Color.green;
            }
            else {
                leftSidebar.color = Color.red;
                rightSidebar.color = Color.red;
            }
        }

        return true;
    }

    public int submitWord() {
        // Check validity and get word score
        // If valid, clear list

        int score = evaluator.SubmitWord(word);

        Int32.TryParse(scoreText.text, out int currentScore);
        scoreText.text = (currentScore + score).ToString();

        if (score > 0)
        {
            ScoreUtils.addWordToCollection(word, score);
        }

        letters.Clear();
        word = "";
        foreach (SpriteRenderer sr in sprites) sr.sprite = defaultSprite;
        currentLetterBox = 0;

        float timeGained = Mathf.Clamp(score / 50, 0, timerClass.GetMaxTime() - timerClass.GetTime());
        timerClass.AddTime(timeGained);
        Debug.Log("Time gained: " + timeGained);

        leftSidebar.color = Color.gray;
        rightSidebar.color = Color.gray;

        return score;
    }
}
