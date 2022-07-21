using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Powerup/Wildcard")]
public class Wildcard : Powerup
{
    [SerializeField] private LetterClass letterClass;

    public LetterClass LetterClass { get { return letterClass; }}

    public override bool Collect()
    {
        return Activate();
    }
    
    public override bool Activate()
    {
        GlobalVariables.word.addLetter(letterClass);
        return true;
    }
}
