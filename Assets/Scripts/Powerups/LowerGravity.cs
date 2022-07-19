using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Powerup/LowerGravity")]
public class LowerGravity : Powerup
{
    [SerializeField]
    private float duration;

    private float time = -1.0f;

    public override bool Collect()
    {
        return Activate();
    }

    public override bool Activate()
    {
        GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.75f;

        return true;
    }
}

