using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPlatform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public int letterVal;
    public int score;

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

    /* storing letter value */
    public int LetterValue {
        get {
            return letterVal;
        }
        set {
            letterVal = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            letterVal = value;
        }
    }
}
