using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dictionaries")]
public class DictionaryObject : ScriptableObject
{
    [SerializeField] private TextAsset wordList;

    private List<string> fullList;

    //List with each word sorted in alphabetical order - for matching anagrams
    private List<string> fullListWithSortedChars;
    [SerializeField] private bool isWordCheck = false;

    private void OnEnable()
    {
        GenerateDictionaries();
    }

    public void GenerateDictionaries()
    {
        string allWords = wordList.text;
        fullList = new List<string>();

        if (isWordCheck)
        {
            
            fullListWithSortedChars = new List<string>();
            foreach (string word in allWords.Split("\n"[0]))
            {
                fullList.Add(word.Trim());
                fullListWithSortedChars.Add(GetWordWithSortedChars(word.Trim()));
            }
        }
        else
        {
            foreach (string word in allWords.Split("\n"[0]))
            {
                fullList.Add(word.Trim());
            }
        }
    }

    public List<string> GetFullDictionary()
    {
        return fullList;
    }

    public bool VerifyWord(string word)
    {
        return fullList.Contains(word.ToUpper());
    }
    
    public string GetRandomWord()
    {

        return fullList[Random.Range(0, fullList.Count - 1)];
    }

    public bool VerifyWordAnagram(string word)
    {
        string sortedWord = GetWordWithSortedChars(word.ToUpper());
        return fullListWithSortedChars.Contains(sortedWord);
    }

    public string GetWordWithSortedChars(string input)
    {
        char[] characters = input.ToCharArray();
        System.Array.Sort(characters);
        return new string(characters);
    }
}
