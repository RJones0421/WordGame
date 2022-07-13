using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordEvaluator : MonoBehaviour
{
	[SerializeField] private DictionaryObject dictionaries;

	private Hashtable letterValues;
	
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
	}

	public bool IsValidWord(string word)
	{
		//if anagram powerup is activated check if word matches 
		if(Anagram.isActivated()){
			return dictionaries.VerifyWordAnagram(word);
		}
		return dictionaries.VerifyWord(word);
	}

	public int SubmitWord(string word)
	{
		if (IsValidWord(word))
		{
			return ScoreWord(word);
		}
		else
		{
			return 0;
		}
	}

	private int ScoreWord(string word)
	{
		int score = 0;
		word = word.ToUpper();

		foreach (char letter in word)
		{
			score += (int) letterValues[letter];
		}

		//replace Mathf.Pow with a different multiplier
		score = (int) (score * 100 * FindMultiplier(word.Length));

		return score;
	}

	private float FindMultiplier(int length)
	{
		//1-2 letters no boost
		//3-6 1.1 boost
		//7+ 1.2 boost
		if (length <= 2)
			return 1;
		else if (length <= 6)
			return Mathf.Pow(1.1f, length - 2);
		else
			return 1.4641f + Mathf.Pow(1.2f, length - 6);
	}
}
