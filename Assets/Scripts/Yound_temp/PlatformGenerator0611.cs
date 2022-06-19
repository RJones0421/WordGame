using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlatformGenerator0611 : MonoBehaviour
{
    public GameObject platformPrefab;
   
    public float cameraSpeed;

    /* total number of platforms */
    public int platformMaxCount=100;

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


    // spawning platforms in sections with pre-defined height (instead of totally random position)  
    private int[] MaxCountList={2,3,4,5};
    private int MaxCountInSection;
    private float sectionsHeight=2.0f;
    private float sectionsDistance=3.6f;
    private float sectionPositionY=2.0f;
    private int spawnCountInSection=0;
    private int loopCount=0;
    private int loopThreshold=100;
    private Vector2 halfExtents;
 
    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        DeathzoneHeight = screenHeight;
        
        MaxCountInSection=MaxCountList[Random.Range(0,MaxCountList.Length)];
        halfExtents = new Vector2( 1.25f , 1.75f*sectionsHeight);


        /* Initial generation of platforms */
        for (int i=0; i<platformMaxCount; i++){
            /* Position of the new platform */
            if (spawnCountInSection==MaxCountInSection){
                sectionPositionY+=sectionsDistance;
                MaxCountInSection=MaxCountList[Random.Range(0,MaxCountList.Length)];
                spawnCountInSection=0;
            }
            
            spawnPosition.x=spawnPositionWidthScale*Random.Range(-1.0f*screenWidth,1.0f*screenWidth);
            spawnPosition.y=sectionPositionY+spawnPositionHeightScale*Random.Range(0f, sectionsHeight);
            
            // avoid collision when spawning 
            loopCount=0;
            Physics.SyncTransforms();
            while(Physics2D.OverlapBox(spawnPosition, halfExtents, 0, (1 << 7)) != null && loopCount<loopThreshold ) {  
                spawnPosition.x=spawnPositionWidthScale*Random.Range(-1.0f*screenWidth,1.0f*screenWidth);
                spawnPosition.y=sectionPositionY+spawnPositionHeightScale*Random.Range(0f, sectionsHeight);

                loopCount++;
                if (loopCount==loopThreshold){
                    sectionPositionY+=sectionsDistance;
                    spawnPosition.y+=sectionsDistance;
                    MaxCountInSection=MaxCountList[Random.Range(0,MaxCountList.Length)];
                    spawnCountInSection=0;
                }
            }
            
            
            // if(Physics2D.OverlapBox(spawnPosition, new Vector2(2.0f,2.0f), 0, (1 << 7)) != null){
            //     Debug.Log("yo");
            // }
            /* Generate a new platform */
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation
            spawnCountInSection++;


            /* update letter value */
            LetterPlatform letterPlatform = newPlatform.GetComponent<LetterPlatform>();
            int rand = LetterValue(); 
            LetterClass letterObject = letterObjectArray[rand];
            letterPlatform.SpriteRenderer.sprite = letterObject.image;
            letterPlatform.SetLetter(letterObject);

            platformQueue.Enqueue(newPlatform);
        }
        
        /* reference to the lowest platform */
        platform = platformQueue.Dequeue();        
    }

    // Frame-rate independent update
    void FixedUpdate()
    {
        if (Camera.main.transform.position.y - 
            platform.transform.position.y >= DeathzoneHeight*DeathzoneHeightScale){

            if (spawnCountInSection==MaxCountInSection){
                sectionPositionY+=sectionsDistance;
                MaxCountInSection=MaxCountList[Random.Range(0,MaxCountList.Length)];
                spawnCountInSection=0;
            }
        
            spawnPosition.x=spawnPositionWidthScale*Random.Range(-1.0f*screenWidth,1.0f*screenWidth);
            spawnPosition.y=sectionPositionY+spawnPositionHeightScale*Random.Range(0f, sectionsHeight);

            /* update the position to be the highest platform */
            loopCount=0;
            Physics.SyncTransforms();
            while(Physics2D.OverlapBox(spawnPosition, halfExtents, 0, (1 << 7)) != null && loopCount<loopThreshold ) {  
                spawnPosition.x=spawnPositionWidthScale*Random.Range(-1.0f*screenWidth,1.0f*screenWidth);
                spawnPosition.y=sectionPositionY+spawnPositionHeightScale*Random.Range(0f, sectionsHeight);

                loopCount++;
                if (loopCount==loopThreshold){
                    sectionPositionY+=sectionsDistance;
                    spawnPosition.y+=sectionsDistance;
                    MaxCountInSection=MaxCountList[Random.Range(0,MaxCountList.Length)];
                    spawnCountInSection=0;
                }
            }

            platform.transform.position=spawnPosition;
            spawnCountInSection++;


            /* update letter value */
            LetterPlatform letterPlatform = platform.GetComponent<LetterPlatform>();
            int rand = LetterValue();
            LetterClass letterObject = letterObjectArray[rand];
            letterPlatform.SpriteRenderer.sprite = letterObject.image;
            letterPlatform.SetLetter(letterObject);

            /* update the platformQueue */
            platformQueue.Enqueue(platform);
            platform=platformQueue.Dequeue();

            /* reset the color of the sprite */
            letterPlatform.ResetSprite();
        }
    }



    // Update is called once per frame
    // void Update()
    // {
    //     if (Camera.main.transform.position.y - 
    //         platform.transform.position.y >= DeathzoneHeight*DeathzoneHeightScale){

    //         if (spawnCountInSection==platformMaxCountInSection){
    //             sectionPositionY+=sectionsDistance;
    //             spawnCountInSection=0;
    //         }

    //         spawnPosition.x=spawnPositionWidthScale*Random.Range(-1.0f*screenWidth,1.0f*screenWidth);
    //         spawnPosition.y=sectionPositionY+spawnPositionHeightScale*Random.Range(0f, sectionsHeight);

    //         /* update the position to be the highest platform */
    //         loopCount=0;
    //         while(Physics2D.OverlapBox(spawnPosition, new Vector2(1.0f,sectionsHeight), 0, (1 << 7)) != null && loopCount<loopThreshold ) {                
    //             spawnPosition.x=spawnPositionWidthScale*Random.Range(-1.0f*screenWidth,1.0f*screenWidth);
    //             spawnPosition.y=sectionPositionY+spawnPositionHeightScale*Random.Range(0f, sectionsHeight);

    //             loopCount++;
    //             if (loopCount==loopThreshold){
    //                 sectionPositionY+=sectionsDistance;
    //                 spawnCountInSection=0;
    //             }
    //         }

    //         platform.transform.position=spawnPosition;
    //         spawnCountInSection++;


    //         /* update letter value */
    //         LetterPlatform letterPlatform = platform.GetComponent<LetterPlatform>();
    //         int rand = LetterValue();
    //         LetterClass letterObject = letterObjectArray[rand];
    //         letterPlatform.SpriteRenderer.sprite = letterObject.LetterSprite;
    //         letterPlatform.SetLetter(letterObject);

    //         /* update the platformQueue */
    //         platformQueue.Enqueue(platform);
    //         platform=platformQueue.Dequeue();

    //         /* reset the color of the sprite */
    //         letterPlatform.ResetSprite();
    //     }
    // }

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
