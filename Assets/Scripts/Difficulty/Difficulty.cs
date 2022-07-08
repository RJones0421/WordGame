using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Difficulty")]
public class Difficulty : ScriptableObject
{
    [Range(0, 100)] [SerializeField] private int blankFrequency;
    [SerializeField] private float heightScale;
    [SerializeField] private float widthScale;
    [SerializeField] private float minSpawnHeight;
    [SerializeField] private float maxSpawnHeight;

    public int GetBlankFreq()
    {
        return blankFrequency;
    }

    public float GetHeightScale()
    {
        return heightScale;
    }

    public float GetWidthScale()
    {
        return widthScale;
    }

    public float GetMinSpawnHeight()
    {
        return minSpawnHeight;
    }
    
    public float GetMaxSpawnHeight()
    {
        return maxSpawnHeight;
    }
}
