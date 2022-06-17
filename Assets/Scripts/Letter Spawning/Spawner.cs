using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : ScriptableObject
{
    public abstract int GetNextLetter();
}
