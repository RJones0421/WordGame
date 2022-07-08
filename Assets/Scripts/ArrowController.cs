using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    public GameObject leftArrow;
    public GameObject rightArrow;

    private Image arrowLeftImage;
    private Image arrowRightImage;

    void Awake()
    {
        arrowLeftImage = leftArrow.GetComponent<Image>();
        arrowRightImage = rightArrow.GetComponent<Image>();
    }
    void Update()
    {
        
    }

    public void RecolorArrows(Color c)
    {
        arrowLeftImage.color = c;
        arrowRightImage.color = c;
    }
}
