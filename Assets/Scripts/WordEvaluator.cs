using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordEvaluator : MonoBehaviour
{
	public TextAsset dictionary;
	
	private Hashtable wordList;
	
    void Start()
    {
        string allWords = dictionary.text;
		wordList = new Hashtable();
		
		foreach (string word in allWords.Split("\n"[0]))
		{
			wordList.Add(word, true);
		}
    }

	public bool IsValidWord(string word)
	{
		return wordList.Contains(word.ToUpper());
	}

    void Update()
    {
        Debug.Log(IsValidWord("test"));
    }
}
