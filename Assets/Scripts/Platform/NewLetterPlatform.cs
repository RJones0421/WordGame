using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLetterPlatform : Platform
{
    private SpriteRenderer spriteRenderer;
    public LetterClass letter;
    private Word word;
    private bool isCollected = false;

    public override void Activate()
    {
        CollectLetter();
    }

    private void OnEnable()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
    }

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

    public void SetLetter(LetterClass letter)
    {
        this.letter = letter;
    }

    public void DarkenSprite()
    {
        spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
        isCollected = true;
    }

    public void ResetSprite()
    {
        spriteRenderer.color = Color.white;
        isCollected = false;
    }

    public void CollectLetter()
    {
        if (letter.Letter == '_') return;
        if (!isCollected)
        {
            word.addLetter(letter);
            DarkenSprite();
        }
    }
}
