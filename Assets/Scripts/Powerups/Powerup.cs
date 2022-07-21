using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerup")]
public abstract class Powerup : Collectible
{
    [SerializeField] private string power;

    public string Power { get { return power; } }

    public abstract bool Activate();
}
