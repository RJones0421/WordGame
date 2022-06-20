using UnityEngine;
using System.Collections;

public class Resurrect : Powerup
{
    public override bool Collect()
    {
        return Activate();
    }

    public override bool Activate()
    {
        // Uhhh idk yet lol
        return false;
    }
}
