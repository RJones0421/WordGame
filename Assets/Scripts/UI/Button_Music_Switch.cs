using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Music_Switch : MonoBehaviour
{

	public Sprite offSprite;
	public Sprite tempSprite;
	public Image original;

	public AudioMixerVolumeController volController;

    private void Awake()
    {
		if (volController.IsMuted())
        {
			original.sprite = offSprite;
		}

		else
        {
			original.sprite = tempSprite;
		}
	}

	public void ChangeImage()
	{
		if (original.sprite == tempSprite)
		{

			original.sprite = offSprite;

		}

		else
		{

			original.sprite = tempSprite;

		}
	}
}



