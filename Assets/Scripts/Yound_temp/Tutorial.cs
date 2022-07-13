using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public LetterClass[] letterObjectArray;
    private int[] letterValues={8,21,7}; // H, U, G;

    public NewLetterPlatform[] letterPlatform;
    public GameObject letterArrow;
    
    void Awake()
    {
        for(int i=0;i<3;i++){
            letterPlatform[i].SpriteRenderer.sprite = letterObjectArray[letterValues[i]].image;
            letterPlatform[i].ActivateArrow();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
