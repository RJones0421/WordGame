using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private LetterClass letterObject;

    public SpriteRenderer SpriteRenderer {
        get {
            if (spriteRenderer == null) {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
        set {
            spriteRenderer = value;
        }
    }
    public LetterClass LetterObject
    {
        get
        {
            return letterObject;
        }
        set
        {
            letterObject = value;
        }
    }
}
