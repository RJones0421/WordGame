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
    private Dictionary<string,List<string>> Anagrams; 

    private void OnEnable()
    {
        GenerateDictionaries();
    }

    public void GenerateDictionaries()
    {
        string allWords = wordList.text;
        fullList = new List<string>();
        Anagrams = new Dictionary<string, List<string>>();

        if (isWordCheck)
        {
            
            fullListWithSortedChars = new List<string>();
            foreach (string word in allWords.Split("\n"[0]))
            {
                string anagram = GetWordWithSortedChars(word.Trim());
                fullList.Add(word.Trim());
                if(Anagrams.ContainsKey(anagram)){
                    
                    Anagrams[anagram].Add(word.Trim());
                }
                else{
                    Anagrams.Add(anagram,new List<string>{word.Trim()});
                }
                fullListWithSortedChars.Add(GetWordWithSortedChars(word.Trim()));
            }
        }
        else
        {
            foreach (string word in allWords.Split("\n"[0]))
            {
                string anagram = GetWordWithSortedChars(word.Trim());
                fullList.Add(word.Trim());
                if(Anagrams.ContainsKey(anagram)){
                    
                     Anagrams[anagram].Add(word.Trim());
                }
                else{
                    Anagrams.Add(anagram,new List<string>{word.Trim()});
                }
                fullListWithSortedChars.Add(GetWordWithSortedChars(word.Trim()));
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
    public string getAnagram(string input){
        char[] characters = input.ToCharArray();
        System.Array.Sort(characters);
        string anagram = new string(characters);
        List<string> anagramWords = Anagrams.GetValueOrDefault(anagram, new List<string>{""});
        return anagramWords[0];
    }
}
