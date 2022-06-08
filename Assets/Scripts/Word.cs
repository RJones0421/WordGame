using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{

    [SerializeField] WordEvaluator evaluator;

    private List<LetterClass> letters = new List<LetterClass>();

    public void addLetter(LetterClass newLetter) {
        letters.Add(newLetter);
    }

    public int submitWord() {
        // Make list into string

        string word = "";

        foreach (LetterClass l in letters) word += l.Letter;

        // Check validity and get word score
        // If valid, clear list

        int score = evaluator.SubmitWord(word);

        if (score != 0){
            letters.Clear();
            ScoreUtils.addWordToCollection(word,score);
        }

        return score;

    }
}
