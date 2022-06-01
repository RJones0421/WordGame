using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_generator : MonoBehaviour
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

    
    public float spawnPositionWidthScale = 0.8f;
    public float spawnPositionHeightScale = 1.0f;

   


    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        DeathzoneHeight = 2*screenHeight;


        Debug.Log(screenWidth);   

        /* Initial generation of platforms */
        for (int i=0; i<platformMaxCount; i++){
            /* Position of the new platform */
            spawnPosition.y+=Random.Range(spawnPositionHeightScale*1.2f,spawnPositionHeightScale*1.4f);
            spawnPosition.x=Random.Range(-1.0f*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);

            /* Generate a new platform */
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation
            
            /* update letter value */
            LetterPlatform letterPlatform = newPlatform.GetComponent<LetterPlatform>();
            int rand = LetterValue(); 
            letterPlatform.SpriteRenderer.sprite = letterPlatform.spriteArray[rand];
            letterPlatform.LetterValue = rand;

            platformQueue.Enqueue(newPlatform);
        }
        
        /* reference to the lowest platform */
        platform = platformQueue.Dequeue();

        /* Simulate the movement of camera */
        StartCoroutine(cameraMove()); // WaitForSeconds() requires to work inside StartCoroutine()
        IEnumerator cameraMove(){

            //for (int t=0;t<1000;t++){
            while(true){    
                /* movement of camera */
                Camera.main.transform.position += new Vector3(0f, cameraSpeed * Time.deltaTime, 0f);

                /* Wait for 0.25 seconds */
                //yield return new WaitForSecondsRealtime(0.25f);
                yield return null;
            }   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y - 
            platform.transform.position.y >= DeathzoneHeight*DeathzoneHeightScale){

            // Debug.Log("Updating platform");        

            /* update the position to be the highest platform */
            spawnPosition.y+=Random.Range(spawnPositionHeightScale*1.2f,spawnPositionHeightScale*1.4f);
            spawnPosition.x=Random.Range(-1*spawnPositionWidthScale*screenWidth,spawnPositionWidthScale*screenWidth);     
            platform.transform.position=spawnPosition;

            /* update letter value */
            LetterPlatform letterPlatform = platform.GetComponent<LetterPlatform>();
            int rand = LetterValue(); 
            letterPlatform.SpriteRenderer.sprite = letterPlatform.spriteArray[rand];
            letterPlatform.LetterValue = rand;

            /* update the platformQueue */
            platformQueue.Enqueue(platform);
            platform=platformQueue.Dequeue();
        }
    }

    public int LetterValue() {
        return Random.Range(1,26);
    }
}
