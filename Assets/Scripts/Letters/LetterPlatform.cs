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

    public void DarkenSprite()
    {
        spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
    }

    public void ResetSprite()
    {
        spriteRenderer.color = Color.white;
    }
}
