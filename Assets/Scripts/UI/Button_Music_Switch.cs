using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Music_Switch : MonoBehaviour
{

	public Sprite offSprite;
	public Sprite tempSprite;
	public Image original;

	public GameObject controlSprite;

	void Start()
    {

    	controlSprite.SetActive(false);

    }

	void Update()
    {

		if (Input.GetKeyDown(KeyCode.M))
        {
            controlSprite.SetActive(true);
            print("space key was pressed");
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



