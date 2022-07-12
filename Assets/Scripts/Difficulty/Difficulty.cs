using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    [Range(0, 100)] [SerializeField] private int blankFrequency;
    [SerializeField] private float minSpawnHeight;
    [SerializeField] private float maxSpawnHeight;
    [SerializeField] private DictionaryObject dictionary;

    public int GetBlankFreq()
    {
        return blankFrequency;
    }

    public float GetMinSpawnHeight()
    {
        return minSpawnHeight;
    }
    
    public float GetMaxSpawnHeight()
    {
        return maxSpawnHeight;
    }

    public DictionaryObject GetDictionary()
    {
        return dictionary;
    }
}
