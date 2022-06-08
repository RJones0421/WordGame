using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{

    [SerializeField] private WordEvaluator evaluator;

    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private List<LetterClass> letters = new List<LetterClass>();

    private int currentLetterBox = 0;

    public GameObject timer;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) submitWord();
    }

    public bool addLetter(LetterClass newLetter)
    {
        if (newLetter.Letter == '_') return false;
        if (letters.Count >= 8) return false;

        else
        {
            letters.Add(newLetter);
            sprites[currentLetterBox].sprite = newLetter.LetterSprite;
            currentLetterBox++;
        }

        return true;
    }

    public int submitWord() {
        // Make list into string

        string word = "";

        foreach (LetterClass l in letters) word += l.Letter;

        // Check validity and get word score
        // If valid, clear list

        //Debug.Log("Word: " + word);

        int score = evaluator.SubmitWord(word);

        letters.Clear();
        foreach (SpriteRenderer sr in sprites) sr.sprite = defaultSprite;
        currentLetterBox = 0;

        timer.GetComponent<Timer>().AddTime(score / 100);
        Debug.Log("Time Added: " + score / 100);

        return score;
    }
}
