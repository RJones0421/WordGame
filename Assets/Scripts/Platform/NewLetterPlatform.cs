using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewLetterPlatform : Platform
{
    public Collectible collectible;
    private bool isCollected = false;

    public GameObject letterArrow;

    public override void Activate()
    {
        CollectLetter();
        if (letterArrow){
            letterArrow.SetActive(false);
        }
    }

    public override void ResetSprite()
    {
        base.ResetSprite();

        isCollected = false;
    }

    public void SetLetter(LetterClass letter)
    {
        this.collectible = letter;
    }

    public void SetPowerup(Powerup powerup)
    {
        this.collectible = powerup;
    }

    public void CollectLetter()
    {
        chalkParticles.Play();
        if (!isCollected)
        {
            isCollected = collectible.Collect();
        }

        if (isCollected)
        {
            DarkenSprite();
        }
    }
    public void ActivateArrow()
    {
        if (letterArrow){
            letterArrow.SetActive(true);
        }
    }

    public bool HasBeenCollected()
    {
        return isCollected;
    }
}
