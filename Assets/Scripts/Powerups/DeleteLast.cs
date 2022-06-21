using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup/DeleteLast")]
public class DeleteLast : Powerup
{
    public override bool Collect()
    {
        return Activate();
    }
    
    public override bool Activate()
    {
        GlobalVariables.word.PopLetter();
        return true;
    }
}
