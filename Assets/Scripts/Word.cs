using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{

    [SerializeField] private WordEvaluator evaluator;

    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    [SerializeField] private SpriteRenderer leftSidebar;
    [SerializeField] private SpriteRenderer rightSidebar;

    private List<LetterClass> letters = new List<LetterClass>();

    private int currentLetterBox = 0;

    private string word = "";

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
            word += newLetter.Letter;
            sprites[currentLetterBox].sprite = newLetter.LetterSprite;
            currentLetterBox++;

            // Update Sidebars
            bool valid = evaluator.IsValidWord(word);
            if (valid) {
                //leftSidebar.color = Color.green;
                //rightSidebar.color = Color.green;
            }
            else {
                //leftSidebar.color = Color.red;
                //rightSidebar.color = Color.red;
            }
        }

        return true;
    }

    public int submitWord() {
        // Check validity and get word score
        // If valid, clear list

        int score = evaluator.SubmitWord(word);

        letters.Clear();
        word = "";
        foreach (SpriteRenderer sr in sprites) sr.sprite = defaultSprite;
        currentLetterBox = 0;

        return score;

    }
}
