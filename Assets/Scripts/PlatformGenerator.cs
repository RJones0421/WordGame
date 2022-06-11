using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
   
    public float cameraSpeed;

    /* total number of platforms */
    public int platformMaxCount=20;

    /* all platforms are stored in queue */
    private Queue<GameObject> platformQueue = new Queue<GameObject>();
    GameObject platform;

    private Vector3 spawnPosition = new Vector3();

    private float screenHeight; /* height from center to top/bottom border */
    private float screenWidth; /* height from center to left/right border */

    /* updating/killing platforms when distance from a platform to camera bottom exceeds this number */
    private float DeathzoneHeight; 
    public float DeathzoneHeightScale=1.0f;
    
    public float spawnPositionWidthScale = 0.35f;
    public float spawnPositionHeightScale = 1.0f;

    public LetterClass[] letterObjectArray;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        DeathzoneHeight = screenHeight;


        Debug.Log(screenWidth);   

        /* Initial generation of platforms */
        for (int i=0; i<platformMaxCount; i++){
            /* Position of the new platform */
            while(Physics2D.OverlapCapsule(spawnPosition, new Vector2(2.0f,4.0f), CapsuleDirection2D.Vertical, 0, (1 << 7)) != null) {
                spawnPosition.y+=Random.Range(spawnPositionHeightScale*0.0f,spawnPositionHeightScale*1.4f);
                spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);
            }
            
            /* Generate a new platform */
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation
            
            /* update letter value */
            LetterPlatform letterPlatform = newPlatform.GetComponent<LetterPlatform>();
            int rand = LetterValue(); 
            LetterClass letterObject = letterObjectArray[rand];
            letterPlatform.SpriteRenderer.sprite = letterObject.LetterSprite;
            letterPlatform.SetLetter(letterObject);

            platformQueue.Enqueue(newPlatform);
        }
        
        /* reference to the lowest platform */
        platform = platformQueue.Dequeue();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y - 
            platform.transform.position.y >= DeathzoneHeight*DeathzoneHeightScale){

            // Debug.Log("Updating platform");

            /* update the position to be the highest platform */
            while(Physics2D.OverlapCapsule(spawnPosition, new Vector2(2.0f,4.0f), CapsuleDirection2D.Vertical, 0, (1 << 7)) != null) {
                spawnPosition.y+=Random.Range(spawnPositionHeightScale*0.0f,spawnPositionHeightScale*1.4f);
                spawnPosition.x=Random.Range(-1*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);
            }     
            platform.transform.position=spawnPosition;

            /* update letter value */
            LetterPlatform letterPlatform = platform.GetComponent<LetterPlatform>();
            int rand = LetterValue();
            LetterClass letterObject = letterObjectArray[rand];
            letterPlatform.SpriteRenderer.sprite = letterObject.LetterSprite;
            letterPlatform.SetLetter(letterObject);

            /* update the platformQueue */
            platformQueue.Enqueue(platform);
            platform=platformQueue.Dequeue();

            /* reset the color of the sprite */
            letterPlatform.ResetSprite();
        }
    }

    public int LetterValue() {
        return LetterSpawning.GetLetterStatic();  
    }


    public ScriptableObject[] LetterObjectArray
    {
        get
        {
            return letterObjectArray;
        }
    }
}
