using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtushWordSelector : MonoBehaviour
{
    public TextAsset dictionary;
    private List<string> wordList;

    [SerializeField] private TMP_Text[] letterBoxs = new TMP_Text[5];

    private string currWord; 
    void Awake()
    {
        string allWords = dictionary.text;
        wordList = new List<string>();
		
		foreach (string word in allWords.Split("\n"[0]))
		{
			wordList.Add(word);
            //Debug.Log(word);
		}
        SelectWord();
    }

    public void SelectWord(){
        int pos = Random.Range(0,wordList.Count);
        currWord = wordList[pos];
        Debug.Log(currWord);

        int i = 0;
        while(i < 5){
            letterBoxs[i].text = currWord[i].ToString();
            Debug.Log(letterBoxs[i].text);
            i++;
        }
    }

    void AssignWord(){

    }
}
