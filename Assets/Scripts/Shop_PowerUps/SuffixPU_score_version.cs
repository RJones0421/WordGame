using UnityEngine;
using System.Collections;

public class SuffixPU_score_version : MonoBehaviour
{
    private static bool activated = false;
    private static Hashtable letterValues = new Hashtable();
    private static Word current_word;
    // private Trie trieNodeRoot;


    void Awake()
    {
	    letterValues = new Hashtable();
		letterValues.Add('A', 1);
		letterValues.Add('B', 3);
		letterValues.Add('C', 3);
		letterValues.Add('D', 2);
		letterValues.Add('E', 1);
		letterValues.Add('F', 4);
		letterValues.Add('G', 2);
		letterValues.Add('H', 4);
		letterValues.Add('I', 1);
		letterValues.Add('J', 8);
		letterValues.Add('K', 5);
		letterValues.Add('L', 1);
		letterValues.Add('M', 3);
		letterValues.Add('N', 1);
		letterValues.Add('O', 1);
		letterValues.Add('P', 3);
		letterValues.Add('Q', 10);
		letterValues.Add('R', 1);
		letterValues.Add('S', 1);
		letterValues.Add('T', 1);
		letterValues.Add('U', 1);
		letterValues.Add('V', 4);
		letterValues.Add('W', 4);
		letterValues.Add('X', 8);
		letterValues.Add('Y', 4);
		letterValues.Add('Z', 10);

        // trieNodeRoot = Trie.buildTrie_public(DictionaryObject.GetFullDictionary);
	}


    public static void reset()
    {
        activated = false;
    }

    public static IEnumerator Activate()
    {
        Debug.Log("StopTime Activated");
        yield return new WaitForSeconds(10);
        Debug.Log("Time Returned");
    }

    // input score will be doubled only if the user has activated the shop power up
    public static int DoubleScore(int finalScore)
    {
        Debug.Log("preFinal Score " + finalScore);

       if (activated)
       {
           int new_score = finalScore * 2;
           reset();

            Debug.Log("post final Score " + new_score);
           return new_score;
       }
       else
       {
           reset();
           return finalScore;
       }
    }

    public static void fillTable()
    {
        if (letterValues.Count == 0)
        {
            letterValues.Add('A', 1);
            letterValues.Add('B', 3);
            letterValues.Add('C', 3);
            letterValues.Add('D', 2);
            letterValues.Add('E', 1);
            letterValues.Add('F', 4);
            letterValues.Add('G', 2);
            letterValues.Add('H', 4);
            letterValues.Add('I', 1);
            letterValues.Add('J', 8);
            letterValues.Add('K', 5);
            letterValues.Add('L', 1);
            letterValues.Add('M', 3);
            letterValues.Add('N', 1);
            letterValues.Add('O', 1);
            letterValues.Add('P', 3);
            letterValues.Add('Q', 10);
            letterValues.Add('R', 1);
            letterValues.Add('S', 1);
            letterValues.Add('T', 1);
            letterValues.Add('U', 1);
            letterValues.Add('V', 4);
            letterValues.Add('W', 4);
            letterValues.Add('X', 8);
            letterValues.Add('Y', 4);
            letterValues.Add('Z', 10);
        }
    }

    public static int getWordScore(string current_word)
    {
        fillTable();
        int score = 0;
		current_word = current_word.ToUpper();

		foreach (char letter in current_word)
		{
			score += (int) letterValues[letter];
		}
        return score;
    }

    public static void updateCurrentWord(string new_word, Word word_in)
    {
        fillTable();
        Debug.Log("the current new word " + new_word);
        new_word = new_word.ToUpper();
        string old_word = word_in.word;
        foreach (char letter in old_word)
        {
            word_in.PopLetter();
        }

        foreach (char letter in new_word)
		{
			LetterClass temp_letter = new LetterClass();
            temp_letter.setLetter(letter);
            int letter_score = (int) letterValues[letter];
            temp_letter.setScore(letter_score);

            word_in.addLetter(temp_letter);

		}
    }

    public static string Activate_function()
    {
        current_word = GameObject.Find("Word").GetComponent<Word>();
        string current_word_string = current_word.word;
        int current_word_score = getWordScore(current_word_string);
        Debug.Log("pre activate current word " + current_word_string + " with score " + current_word_score);

        // how do you clear the letter bank
        string new_word = "lol";
        updateCurrentWord(new_word, current_word);
        int new_word_score = getWordScore(current_word.word);
        Debug.Log("post activate current word " + current_word.word + " with score " + new_word_score);


        return current_word_string;
    }
}
