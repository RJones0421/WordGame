using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dictionaries")]
public class DictionaryObject : ScriptableObject
{
    [SerializeField] private TextAsset fullDictionary;
    [SerializeField] private TextAsset commonDictionary;
    public Trie wordSearch;
    public Trie.TrieNode wordSearchRoot;

    private List<string> fullList;
    private List<string> commonList;
    
    private void OnEnable()
    {
        string allWords = fullDictionary.text;
        fullList = new List<string>();
        wordSearch = new Trie();

        foreach (string word in allWords.Split("\n"[0]))
        {
            fullList.Add(word.Trim());
        }
        
        string commonWords = commonDictionary.text;
        commonList = new List<string>();

        foreach (string word in commonWords.Split("\n"[0]))
        {
            commonList.Add(word.Trim());
        }
        wordSearchRoot = wordSearch.buildTrie(commonList);
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

    public string GetRandomFullWord()
    {
        return fullList[Random.Range(0, fullList.Count - 1)];
    }

    public string GetRandomCommonWord()
    {
        return commonList[Random.Range(0, commonList.Count - 1)];
    }
}
