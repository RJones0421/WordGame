using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup/DoubleLetter")]
public class DoubleLetter : Powerup
{
    public override bool Collect()
    {
        return Activate();
    }
    
    public override bool Activate()
    {
        Word word = GlobalVariables.word;
        if (word.GetWordLength() > 0) {
            word.addLetter(word.getLetter(word.GetWordLength()-1));
        }
        return true;
    }
}
