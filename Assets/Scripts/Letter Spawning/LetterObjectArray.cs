using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="LetterArray")]
public class LetterObjectArray : ScriptableObject
{
    [SerializeField] private List<LetterClass> letterObjects = new List<LetterClass>();

    public LetterClass GetLetter(int index) { return letterObjects[index]; }
}
