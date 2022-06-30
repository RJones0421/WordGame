using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="PowerupArray")]
public class PowerupObjectArray : ScriptableObject
{
    [SerializeField] private List<Powerup> powerupObjects = new List<Powerup>();

    public Powerup GetPowerup(int index) { return powerupObjects[index]; }
    public int Count() { return powerupObjects.Count; }
}
