using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;

    public Spawner letterSpawner;

    /* total number of platforms */
    public int platformMaxCount=20;

    /* all platforms are stored in queue */
    private Queue<GameObject> platformQueue = new Queue<GameObject>();
    GameObject bottomPlatform;

    private Vector3 spawnPosition = new Vector3();

    private float screenHeight; /* height from center to top/bottom border */
    private float screenWidth; /* height from center to left/right border */

    /* updating/killing platforms when distance from a platform to camera bottom exceeds this number */
    private float DeathzoneHeight; 
    public float DeathzoneHeightScale=1.0f;
    
    public float spawnPositionWidthScale = 0.35f;
    public float spawnPositionHeightScale = 1.0f;

    public LetterClass[] letterObjectArray;

    private Camera mainCamera;
    public float minHeight = 1.0f;
    public float maxHeight = 1.6f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        screenHeight = mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;
        DeathzoneHeight = screenHeight;

        Debug.Log(screenWidth);

        /* Initial generation of platforms */
        for (int i=0; i<platformMaxCount; i++){
            /* Position of the new platform */
            while(Physics2D.OverlapCapsule(spawnPosition, new Vector2(2.0f,4.0f), CapsuleDirection2D.Vertical, 0, (1 << 7)) != null) {
                spawnPosition.y+=Random.Range(spawnPositionHeightScale*minHeight,spawnPositionHeightScale*maxHeight);
                spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);
            }
            
            /* Generate a new platform */
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation
            
            /* update letter value */
            Platform platform = newPlatform.GetComponent<Platform>();
            LetterClass letterObject = letterObjectArray[GetNextLetter()];
            NewLetterPlatform letterPlatform = platform as NewLetterPlatform;
            if (letterPlatform)
            {
                letterPlatform.SpriteRenderer.sprite = letterObject.image;
                letterPlatform.SetLetter(letterObject);
            }

            platformQueue.Enqueue(newPlatform);
        }
        
        /* reference to the lowest platform */
        bottomPlatform = platformQueue.Dequeue();        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.transform.position.y - 
            bottomPlatform.transform.position.y >= DeathzoneHeight*DeathzoneHeightScale){

            /* update the position to be the highest platform */
            while(Physics2D.OverlapCapsule(spawnPosition, new Vector2(2.0f,4.0f), CapsuleDirection2D.Vertical, 0, (1 << 7)) != null) {
                spawnPosition.y+=Random.Range(spawnPositionHeightScale*minHeight,spawnPositionHeightScale*maxHeight);
                spawnPosition.x=Random.Range(-1*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);
            }     
            bottomPlatform.transform.position=spawnPosition;

            /* update letter value */
            Platform platform = bottomPlatform.GetComponent<Platform>();
            LetterClass letterObject = letterObjectArray[GetNextLetter()];
            NewLetterPlatform letterPlatform = platform as NewLetterPlatform;
            if (letterPlatform)
            {
                letterPlatform.SpriteRenderer.sprite = letterObject.image;
                letterPlatform.SetLetter(letterObject);
            }

            /* update the platformQueue */
            platformQueue.Enqueue(bottomPlatform);
            bottomPlatform=platformQueue.Dequeue();

            /* reset the color of the sprite */
            letterPlatform.ResetSprite();
        }
    }
    
    public int GetNextLetter()
    {
        return letterSpawner.GetNextLetter();
    }

    public void UpdateDifficulty(Difficulty difficulty)
    {
        letterSpawner.blankFrequency = difficulty.GetBlankFreq();
        spawnPositionHeightScale = difficulty.GetHeightScale();
        minHeight = difficulty.GetMinSpawnHeight();
        maxHeight = difficulty.GetMaxSpawnHeight();
    }
}
