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

	public override bool Collect()
	{
		return false;
	}
}
