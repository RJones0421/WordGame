using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawner/Letter")]
public class LetterSpawning : Spawner
{
	// Scrabble tile letter distribution is as follows: A-9, B-2, C-2, D-4, E-12, F-2, G-3, H-2, I-9, J-1, K-1, L-4, M-2, N-6, O-8, P-2, Q-1, R-6, S-4, T-6, U-4, V-2, W-2, X-1, Y-2, Z-1 and Blanks-2.

	// Constant of letters appearing with same distribution from scrabble
	private static string LETTERS = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ";

	// Odds out of 100 for the platform to be without a letter
	//[SerializeField] private int EMPTY_FREQ1 = 15;
	private static int EMPTY_FREQ = 100;
	//private static int RANDOM_FREQ = 20;

	private static bool prev_blank = false;

	// private static string LETTERS_LOWER = "aaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnoooooooooppqrrrrrrssssttttttuuuuvvwwxyyz";

	private string lettersAvailable;

	[SerializeField] LetterObjectArray letterArray;
	
	// In case we want to split between searching for vowels and consonants
	//private string consonants = "BCDFGHJKLMNPQRSTVWXYZ";
    //private string vowels = "AEIOU";
	
    void OnEnable()
    {		
		lettersAvailable = LETTERS;
    }

    public int getRandomLetter() {
		int len = LETTERS.Length;
		return LETTERS[Random.Range(0,len)] - 64;
	}

    public static int GetLetterStatic()
	{
		if (EMPTY_FREQ >= Random.Range(1,101)) {
			return 0;
		}
		int len = LETTERS.Length;
		return LETTERS[Random.Range(0,len)] - 64;
	}

	public int GetLetterNoDoubleBlanks()
	{
		if (prev_blank) {
			prev_blank = false;
			int len = LETTERS.Length;
			return LETTERS[Random.Range(0,len)] - 64;
		}

		if (EMPTY_FREQ <= Random.Range(1,101)) {
			prev_blank = true;
			return 0;
		}
		int leng = LETTERS.Length;
		return LETTERS[Random.Range(0,leng)] - 64;
	}

	public override LetterClass GetNextLetter()
	{
		if (blankFrequency >= Random.Range(1,101)) {
			return letterArray.GetLetter(0);
		}
		int len = LETTERS.Length;
		return letterArray.GetLetter(LETTERS[Random.Range(0,LETTERS.Length)] - 64);
	}
}