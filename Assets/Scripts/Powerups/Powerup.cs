using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup")]
public abstract class Powerup : Collectible
{
    [SerializeField] private string name;

    public string Name { get { return name; } }

    public abstract bool Activate();
}
