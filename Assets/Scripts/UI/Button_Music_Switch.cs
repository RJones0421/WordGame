using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Music_Switch : MonoBehaviour
{

	public Sprite offSprite;
	public Sprite tempSprite;
	public Image original;

    private void Awake()
    {
		if (PlayerPrefs.HasKey("controls"))
        {
			if (PlayerPrefs.GetInt("controls") != 0) ChangeImage();
        }
    }

    public void ChangeImage() {

		if (original.sprite == tempSprite) {

			original.sprite = offSprite;

		}
		
		else {

			original.sprite = tempSprite;

		}

	}

}



