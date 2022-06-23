using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName = "Spawner/Stream")]
public class StreamSpawning : Spawner
{
    [SerializeField] private DictionaryObject dictionaries;

    private Queue<string> queue = new Queue<string>();
    private Queue<char> word = new Queue<char>();
    public string currentWord;
    private char gap = (char) 96;

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
    	currentWord = queue.Dequeue();
    	word = new Queue<char>(currentWord.ToCharArray());
    	word.Enqueue(gap);
    	word.Enqueue(gap);
    	Debug.Log(currentWord);
        queue.Enqueue(GetWord());
    }
	
	public int getLetterQueue1()
	{
		if (word.Count == 0) {
			setQueue();
		}
		return word.Dequeue() - 96;
	}
	
	public override int GetNextLetter()
	{
		if (EMPTY_FREQ >= Random.Range(1, 101))
		{
			return 0;
		}
		
		return getLetterQueue1();
	}

	public string GetCurrentWord()
	{
		return currentWord;
	}
	
}
