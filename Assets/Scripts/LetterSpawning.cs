using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterSpawning : MonoBehaviour
{	
	[SerializeField] private Word currentWord;
	[SerializeField] private DictionaryObject dictionaries;
	

	public Queue<char> queue1;

	public Queue<char> queue2;

	public Trie wordSearchTrie;
	
	// Scrabble tile letter distribution is as follows: A-9, B-2, C-2, D-4, E-12, F-2, G-3, H-2, I-9, J-1, K-1, L-4, M-2, N-6, O-8, P-2, Q-1, R-6, S-4, T-6, U-4, V-2, W-2, X-1, Y-2, Z-1 and Blanks-2.

	// Constant of letters appearing with same distribution from scrabble
	private static string LETTERS = "AAAAAAAAABBCCDDDDEEEEEEEEEEEEFFGGGHHIIIIIIIIIJKLLLLMMNNNNNNOOOOOOOOOPPQRRRRRRSSSSTTTTTTUUUUVVWWXYYZ";

	// Odds out of 100 for the platform to be without a letter
	private static int EMPTY_FREQ = 40;
	private static int RANDOM_FREQ = 20;

	private static bool prev_blank = false;

	// private static string LETTERS_LOWER = "aaaaaaaaabbccddddeeeeeeeeeeeeffggghhiiiiiiiiijkllllmmnnnnnnoooooooooppqrrrrrrssssttttttuuuuvvwwxyyz";

	private string lettersAvailable;

	private string[] randomLetter = new string[26] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

	// In case we want to split between searching for vowels and consonants
	private string consonants = "BCDFGHJKLMNPQRSTVWXYZ";
    private string vowels = "AEIOU";
	
    void Awake()
    {		
		lettersAvailable = LETTERS;

		setQueue1();
		setQueue2();
    }

	public void setQueue1() {
		string currentLetter = currentWord.getLetter();
		Debug.Log("current letter is " + currentLetter);
		char[] charArr;
		if(currentLetter.Length == 0){
		 charArr = dictionaries.GetRandomFullWord().ToCharArray();
		}
		else{
			string allLetters = "";
			List<List<string>> suggestions = dictionaries.wordSearch.suggestedWords(currentLetter.ToLower(),dictionaries.wordSearchRoot);
			//Debug.Log(suggestions[0][0]);
			foreach(List<string> list in suggestions){
				foreach (string word in list)
				{
					allLetters += word;
				}
			}
			Debug.Log(allLetters);
		    charArr = allLetters.ToUpper().ToCharArray();	
		}
		queue1 = new Queue<char>(charArr);
	}

	public void setQueue2() {
		string currentLetter = currentWord.getLetter();
		char[] charArr;
		if(currentLetter.Length == 0){
		 charArr = dictionaries.GetRandomFullWord().ToCharArray();
		}
		else{
			string allLetters = "";
			List<List<string>> suggestions = dictionaries.wordSearch.suggestedWords(currentLetter.ToLower(),dictionaries.wordSearchRoot);
			//Debug.Log(suggestions[0][0]);
			foreach(List<string> list in suggestions){
				foreach (string word in list)
				{
					allLetters += word;
				}
			}
			Debug.Log(allLetters);
		    charArr = allLetters.ToUpper().ToCharArray();	
		}
		queue1 = new Queue<char>(charArr);
	}

	public int getLetterQueue1() {
		if (queue1.Count == 0) {
			setQueue1();
		}
		return queue1.Dequeue() - 64;
	}

	public int getLetterQueue2() {
		if (queue2.Count == 0) {
			setQueue1();
		}
		return queue2.Dequeue() - 64;
	}

	public int getRandomLetter() {
		int len = LETTERS.Length;
		return LETTERS[Random.Range(0,len)] - 64;
	}

	public int getStream1() {
		if (EMPTY_FREQ >= Random.Range(1,101)) {
			return 0;
		}
		if (RANDOM_FREQ >= Random.Range(1,101)) {
			return getRandomLetter();
		}
		return getLetterQueue1();
	}

	public int getStream2() {
		if (EMPTY_FREQ >= Random.Range(1,101)) {
			return 0;
		}
		if (RANDOM_FREQ >= Random.Range(1,101)) {
			return getRandomLetter();
		}
		return getLetterQueue2();
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
		if (EMPTY_FREQ >= Random.Range(1,101)) {
			return 0;
		}
		int len = LETTERS.Length;
		return LETTERS[Random.Range(0,len)] - 64;
	}

	public static int GetLetterNoDoubleBlanks()
	{
		if (prev_blank) {
			prev_blank = false;
			int len = LETTERS.Length;
			return LETTERS[Random.Range(0,len)] - 64;
		}

		if (EMPTY_FREQ >= Random.Range(1,101)) {
			prev_blank = true;
			return 0;
		}
		int leng = LETTERS.Length;
		return LETTERS[Random.Range(0,leng)] - 64;
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
		// string currentLetter = currentWord.getLetter();
		// Debug.Log("current letter is " + currentLetter);
    }
}