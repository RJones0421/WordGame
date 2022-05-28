using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordEvaluator : MonoBehaviour
{
	
	public TextAsset dictionary;
	
	private List<string> wordList;
	
    void Start()
    {
        string allWords = dictionary.text;
		wordList = new List<string>();
		wordList.AddRange(allWords.Split("\n"[0]));
    }

    void Update()
    {
        
    }
}
