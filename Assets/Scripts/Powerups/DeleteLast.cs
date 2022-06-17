using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup/DeleteLast")]
public class DeleteLast : Powerup
{

    [SerializeField] private Word word;

    public override bool Activate()
    {
        word.PopLetter();
        return true;
    }
}
