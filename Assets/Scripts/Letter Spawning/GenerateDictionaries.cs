using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDictionaries : MonoBehaviour
{
    [SerializeField] private DictionaryObject dictionaries;
    
    private void Awake()
    {
        dictionaries.GenerateDictionaries();
    }
}
