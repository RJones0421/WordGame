using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup/TwoX")]
public class TwoX : Powerup
{
    public override bool Collect()
    {
        return Activate();
    }

    public override bool Activate()
    {
        GlobalVariables.word.setMultiplier(2);
        return true;
    }
}
