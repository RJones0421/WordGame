using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGeneratorWeek4 : MonoBehaviour
{
    public GameObject platformPrefab;
   
    public float cameraSpeed;

    /* total number of platforms */
    public int platformTotalCount=120;
    public int platformMaxCountInRow=7;

    private int spawnCountInRow=0;
    private int loopCount=0;
    private int loopThreshold=30;

    /* all platforms are stored in queue */
    private Queue<GameObject> platformQueue = new Queue<GameObject>();
    GameObject platform;

    private Vector3 spawnPosition = new Vector3(0,2.0f,0);

    private float screenHeight; /* height from center to top/bottom border */
    private float screenWidth; /* height from center to left/right border */

    /* updating/killing platforms when distance from a platform to camera bottom exceeds this number */
    private float DeathzoneHeight; 
    public float DeathzoneHeightScale=1.25f;
    
    public float spawnPositionWidthScale = 0.35f;
    public float spawnPositionHeightScale = 1.0f;

    public LetterClass[] letterObjectArray;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        DeathzoneHeight = 2*screenHeight;


        if (platformMaxCountInRow>7)
            platformMaxCountInRow=7;
        else if(platformMaxCountInRow<=0)
            platformMaxCountInRow=1;
        
        /* Initial generation of platforms */
        for (int i=0; i<platformTotalCount; i++){
            /* Position of the new platform */
            if (spawnCountInRow==platformMaxCountInRow){
                spawnPosition.y+=spawnPositionHeightScale*3.7f;
                spawnCountInRow=0;
            }
            
            spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);

            // avoid collision when spawning 
            loopCount=0;
            while(Physics2D.OverlapCapsule(spawnPosition, new Vector2(1.5f,0.0f), CapsuleDirection2D.Vertical, 0, (1 << 7)) != null && loopCount<loopThreshold ) {                
                spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);
                loopCount++;
                if (loopCount==loopThreshold){
                    spawnPosition.y+=spawnPositionHeightScale*3.7f;
                    spawnCountInRow=0;
                }
            }
            
            /* Generate a new platform */
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation
            spawnCountInRow++;
            
            /* update letter value */
            LetterPlatform letterPlatform = newPlatform.GetComponent<LetterPlatform>();
            int rand=0;
            if (spawnCountInRow>1){ // first platform in the row is a blank
                while(rand==0){
                    rand = LetterValue(); 
                }
            }
               
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
            
            if (spawnCountInRow==platformMaxCountInRow){
                spawnPosition.y+=spawnPositionHeightScale*3.7f;
                spawnCountInRow=0;
            }

            spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);

            /* update the position to be the highest platform */
            loopCount=0;
            while(Physics2D.OverlapCapsule(spawnPosition, new Vector2(1.5f,0.0f), CapsuleDirection2D.Vertical, 0, (1 << 7)) != null && loopCount<loopThreshold ) {                
                spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);
                loopCount++;
                if (loopCount==loopThreshold){
                    spawnPosition.y+=spawnPositionHeightScale*3.7f;
                    spawnCountInRow=0;
                }
            }

            platform.transform.position=spawnPosition;
            spawnCountInRow++;

            /* update letter value */
            LetterPlatform letterPlatform = platform.GetComponent<LetterPlatform>();

            int rand=0;
            if (spawnCountInRow>1){ // first platform in the row is a blank
                while(rand==0){
                    rand = LetterValue(); 
                }
            }
               
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
