using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dictionaries")]
public class DictionaryObject : ScriptableObject
{
    [SerializeField] private TextAsset fullDictionary;
    [SerializeField] private TextAsset commonDictionary;

    private List<string> fullList;
    private List<string> commonList;

    //List with each word sorted in alphabetical order - for matching anagrams
    private List<string> fullListWithSortedChars; 

    private void OnEnable()
    {
        GenerateDictionaries();
    }

    public void GenerateDictionaries()
    {
        string allWords = fullDictionary.text;
        fullList = new List<string>();
        fullListWithSortedChars = new List<string>();

        foreach (string word in allWords.Split("\n"[0]))
        {
            fullList.Add(word.Trim());
            fullListWithSortedChars.Add(GetWordWithSortedChars(word.Trim()));
        }
        
        string commonWords = commonDictionary.text;
        commonList = new List<string>();

        foreach (string word in commonWords.Split("\n"[0]))
        {
            commonList.Add(word.Trim());
        }
    }

    public List<string> GetFullDictionary()
    {
        return fullList;
    }

    public List<string> GetCommonDictionary()
    {
        return commonList;
    }

    public bool VerifyWord(string word)
    {
        return fullList.Contains(word.ToUpper());
    }

    public bool VerifyWordAnagram(string word)
    {
        string sortedWord = GetWordWithSortedChars(word.ToUpper());
        return fullListWithSortedChars.Contains(sortedWord);
    }

    public string GetRandomFullWord()
    {
        return fullList[Random.Range(0, fullList.Count - 1)];
    }

    public string GetRandomCommonWord()
    {
        return commonList[Random.Range(0, commonList.Count - 1)];
    }

    public string GetWordWithSortedChars(string input)
    {
        char[] characters = input.ToCharArray();
        System.Array.Sort(characters);
        return new string(characters);
    }
}
