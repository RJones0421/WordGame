using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup")]
public abstract class Powerup : ScriptableObject
{
    [SerializeField] private string name;

    [SerializeField] private Sprite icon;

    public string Name { get { return name; }}

	public Sprite Icon { get { return icon; } }

    public abstract bool Activate();
}
