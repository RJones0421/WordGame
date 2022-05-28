using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordEvaluator : MonoBehaviour
{
	public TextAsset dictionary;
	
	private Hashtable wordList;
	
	private Hashtable letterValues;
	
    void Start()
    {
        string allWords = dictionary.text;
		wordList = new Hashtable();
		
		foreach (string word in allWords.Split("\n"[0]))
		{
			wordList.Add(word, true);
		}
		
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
