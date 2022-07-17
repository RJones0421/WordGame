using UnityEngine;
using System.Collections;
using System.Text;

// Not used in class build
public class Swap : Powerup
{
    public override bool Collect()
    {
        return Activate();
    }

    public override bool Activate()
    {
        Word word = GameObject.Find("Player").GetComponent<PlayerController>().word;
        if (word.currentLetterBox > 1)
        {
            Sprite tempSprite = word.sprites[word.currentLetterBox - 2].sprite;
            word.sprites[word.currentLetterBox - 2].sprite = word.sprites[word.currentLetterBox - 1].sprite;
            word.sprites[word.currentLetterBox - 1].sprite = tempSprite;

            LetterClass tempLetterClass = word.letters[word.currentLetterBox - 2];
            word.letters[word.currentLetterBox - 2] = word.letters[word.currentLetterBox - 1];
            word.letters[word.currentLetterBox - 1] = tempLetterClass;

            StringBuilder stringBuilder = new StringBuilder(word.word);
            stringBuilder[word.currentLetterBox - 2] = word.word[word.currentLetterBox - 1];
            stringBuilder[word.currentLetterBox - 1] = word.word[word.currentLetterBox - 2];
            word.word = stringBuilder.ToString();

            word.UpdateSidebars();
        }

        return true;
    }
}
