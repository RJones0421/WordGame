using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawning : MonoBehaviour
{
	
	// Scrabble tile letter distribution is as follows: A-9, B-2, C-2, D-4, E-12, F-2, G-3, H-2, I-9, J-1, K-1, L-4, M-2, N-6, O-8, P-2, Q-1, R-6, S-4, T-6, U-4, V-2, W-2, X-1, Y-2, Z-1 and Blanks-2.

	// Constant of letters appearing with same distribution from scrabble
	private static string LETTERS = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ";

	private static string LETTERS_LOWER = "aaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnoooooooooppqrrrrrrssssttttttuuuuvvwwxyyz";

	private string lettersAvailable;

	private string[] randomLetter = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

	// In case we want to split between searching for vowels and consonants
	private string consonants = "BCDFGHJKLMNPQRSTVWXYZ";
    private string vowels = "AEIOU";
	
    void Awake()
    {		
		lettersAvailable = LETTERS;
    }

	// random returns letter with same odds for all letters
    public char GetLetter1()
    {
        int num = Random.Range(0, 26);
        char letter = (char)('a' + num);
        return letter;
    }

	// random returns letter, removes letter from string (resets once empty)
	public char GetLetter2()
	{
		int len = lettersAvailable.Length;
		if (len == 0) {
			len = resetLetters();
		}

		int index = Random.Range(0,len);
		char result = lettersAvailable[index];
		lettersAvailable = lettersAvailable.Remove(index,1);

		return result;
	}

	// random letter, does not remove letter
	public char GetLetter3()
	{
		int len = lettersAvailable.Length;
		return lettersAvailable[Random.Range(0,len)];
	}

	public static int GetLetterStatic()
	{
		int len = LETTERS.Length;
		return LETTERS[Random.Range(0,len)] - 64;
	}

	// random vowel
	public char GetVowel()
	{
		int len = vowels.Length;
		return vowels[Random.Range(0,len)];
	}

	// random consonant
	public char GetConsonant()
	{
		int len = consonants.Length;
		return consonants[Random.Range(0,len)];
	}

	// resets string of available letters
	public int resetLetters()
	{
		lettersAvailable = LETTERS;
		return lettersAvailable.Length;
	}

    void Update()
    {
    }
}