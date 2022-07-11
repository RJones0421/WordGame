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

    [SerializeField] private List<GameObject> resetText;
    [SerializeField] private List<GameObject> submitText;

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

        if(c == Color.red){  
            ActivateGameObjects(resetText);
            DeactivateGameObjects(submitText);
        }
        else if(c == Color.green){
            ActivateGameObjects(submitText);
            DeactivateGameObjects(resetText);
        }
        else{
            DeactivateGameObjects(resetText);
            DeactivateGameObjects(submitText);
        }
    }

    private void ActivateGameObjects(List<GameObject> l){

        foreach (GameObject gb in l)
        {  
            gb.SetActive(true);
        }
    }
    private void DeactivateGameObjects(List<GameObject> l){

        foreach (GameObject gb in l)
        {  
            gb.SetActive(false);
        }
    }
}
