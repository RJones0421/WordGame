using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLetterPlatform : Platform
{
    private SpriteRenderer spriteRenderer;
    public Collectible collectible;
    private bool isCollected = false;

    public override void Activate()
    {
        CollectLetter();
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
        this.collectible = letter;
    }

    public void DarkenSprite()
    {
        spriteRenderer.color = new Color(0.3f, 0.3f, 0.3f);
    }

    public void ResetSprite()
    {
        spriteRenderer.color = Color.white;
        isCollected = false;
    }

    public void CollectLetter()
    {
        if (!isCollected)
        {
            isCollected = collectible.Collect();
        }

        if (isCollected)
        {
            DarkenSprite();
        }
        /*
        if (collectible.Letter == '_') return;
        if (!isCollected)
        {
            word.addLetter(collectible);
            DarkenSprite();
        }
        */
    }
}
