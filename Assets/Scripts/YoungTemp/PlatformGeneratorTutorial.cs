using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorTutorial : MonoBehaviour
{
    
    public GameObject platformPrefab;

    public LetterSpawning letterSpawner;
   
    public float cameraSpeed;

    /* total number of platforms */
    public int platformMaxCount=20;

    
    public GameObject platformT;
    public GameObject platformO;
    public GameObject platformE;

    /* all platforms are stored in queue */
    private Queue<GameObject> platformQueue = new Queue<GameObject>();
    GameObject platform;

    private Vector3 spawnPosition = new Vector3();

    private float screenHeight; /* height from center to top/bottom border */
    private float screenWidth; /* height from center to left/right border */

    /* updating/killing platforms when distance from a platform to camera bottom exceeds this number */
    private float DeathzoneHeight; 
    public float DeathzoneHeightScale=1.25f;
    
    public float spawnPositionWidthScale = 0.35f;
    public float spawnPositionHeightScale = 1.0f;

    public LetterClass[] letterObjectArray;

    // Start is called before the first frame update
    void Awake()
    {

        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        DeathzoneHeight = 2*screenHeight;

            /* update letter value */
        LetterPlatform letterPlatformT = platformT.GetComponent<LetterPlatform>();
        LetterClass letterObjectT = letterObjectArray[0];
        letterPlatformT.SpriteRenderer.sprite = letterObjectT.LetterSprite;
        letterPlatformT.SetLetter(letterObjectT);

        LetterPlatform letterPlatformO = platformO.GetComponent<LetterPlatform>();
        LetterClass letterObjectO = letterObjectArray[1];
        letterPlatformO.SpriteRenderer.sprite = letterObjectO.LetterSprite;
        letterPlatformO.SetLetter(letterObjectO);

        LetterPlatform letterPlatformE = platformE.GetComponent<LetterPlatform>();
        LetterClass letterObjectE = letterObjectArray[2];
        letterPlatformE.SpriteRenderer.sprite = letterObjectE.LetterSprite;
        letterPlatformE.SetLetter(letterObjectE);


    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void LetterValue() {
       // return letterSpawner.getStream1(getStream1);
    }


    public ScriptableObject[] LetterObjectArray
    {
        get
        {
            return letterObjectArray;
        }
    }
}
