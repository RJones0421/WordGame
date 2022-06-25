using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : ScriptableObject
{
    [Range(0, 100)] public int blankFrequency;
    public abstract LetterClass GetNextLetter();
}
