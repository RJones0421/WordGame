using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Letter")]
public class LetterClass : Collectible
{
	[SerializeField]
	private char letter;

	[SerializeField]
	private int score;

	public char Letter { get { return letter; }}

	public int Score { get { return score; } }


	public void setLetter(char char_in)
	{
		letter = char_in;
	}

	public void setScore(int score_in) {
		score = score_in;
	}


	public override bool Collect()
	{
		if (letter == '_')
		{
			return false;
		}


		return GlobalVariables.word.addLetter(this);
	}
}
