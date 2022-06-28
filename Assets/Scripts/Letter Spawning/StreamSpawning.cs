using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Spawner/Stream")]
public class StreamSpawning : Spawner
{
    [SerializeField] private DictionaryObject dictionaries;

	[SerializeField] LetterObjectArray letterArray;

    private Queue<string> queue = new Queue<string>();
    private Queue<char> word = new Queue<char>();
    public string currentWord;
    private char gap = (char) 123;

	//private Word guess = GameObject.Find("Word").GetComponent<Word>();
	private Trie trie = new Trie();
	private Trie.TrieNode root;

    public bool isCommon;
    private TMP_Text text;

    [SerializeField] [Range(0, 100)] private int EMPTY_FREQ;

    void OnEnable()
	{
		for (int i = 0; i < 4; i++)
		{
			queue.Enqueue(GetWord());
		}
		setQueue();
		//guess = GameObject.Find("Word").GetComponent<Word>();
		//trie = dictionaries.wordSearch;
	}

	private string GetWord()
	{
		if (isCommon)
		{
			return dictionaries.GetRandomCommonWord();
		}
		else
		{
			return dictionaries.GetRandomFullWord();
		}
	}
	
	public void setQueue()
	{
		//this.trie = dictionaries.wordSearch;
    	Word guess = GameObject.Find("Word").GetComponent<Word>();
		currentWord = queue.Dequeue();
     	word = new Queue<char>(currentWord.ToCharArray());
    	word.Enqueue(gap);
    	currentWord = queue.Dequeue();
        word = new Queue<char>(currentWord.ToCharArray());
    	word.Enqueue(gap);
    	Debug.Log(currentWord);
		string letters = guess.GetWord();
		List<List<string>> suggestions = trie.suggestedProducts(dictionaries.GetCommonDictionary(),guess.GetWord());
		Debug.Log(suggestions[0][0]);
		//Debug.Log(suggestions[0][0]);
        queue.Enqueue(GetWord());
    }
	
	public int getLetterQueue1()
	{
		if (word.Count == 0) {
			setQueue();
			GlobalVariables.updateWordChangeHeight = true;
		}
		return word.Dequeue() - 96;
	}
	
	public override LetterClass GetNextLetter()
	{
		if (EMPTY_FREQ >= Random.Range(1, 101))
		{
			return letterArray.GetLetter(0);
		}
		
		return letterArray.GetLetter(getLetterQueue1());
	}
	
}
