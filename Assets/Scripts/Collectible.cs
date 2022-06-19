using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectible : ScriptableObject
{
    public Sprite image;

    public abstract bool Collect();
}
