using UnityEngine;
using System.Collections;

public class JumpPlatform : Platform
{
    public override void Activate()
    {
        Platform.activated = true;

        GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.7f;
    }
}
