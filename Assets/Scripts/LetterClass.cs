using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Letter")]
public class LetterClass : ScriptableObject
{
	[SerializeField]
	private char letter;
	
	[SerializeField]
	private Sprite letterSprite;
	
	[SerializeField]
	private int score;

	public char Letter { get { return letter; }}
}
