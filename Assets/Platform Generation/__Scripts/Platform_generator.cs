using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_generator : MonoBehaviour
{
    public GameObject platformPrefab;
    
    /* total number of platforms */
    public int platform_maxCount=15;

    /* all platforms are stored in queue */
    private Queue<GameObject> platformQueue = new Queue<GameObject>();
    GameObject platform;

    private Vector3 spawnPosition = new Vector3();

    /* height from center to top/bottom border */
    private float screenHeight;

    public float cameraSpeed;

    public float sizeScale = 1.0f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;

        /* Initial generation of platforms */
        for (int i=0; i<platform_maxCount; i++){
            /* Position of the new platform */
            spawnPosition.y+=Random.Range(sizeScale*1.0f,sizeScale*1.5f);
            spawnPosition.x=Random.Range(sizeScale*-2.0f,sizeScale*2.0f);
            
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.transform.position.y - 
            platform.transform.position.y >= 1.2*screenHeight){

            // Debug.Log("Updating platform");        

            /* update the position to be the highest platform */
            spawnPosition.y+=Random.Range(sizeScale*1.0f,sizeScale*1.5f);
            spawnPosition.x=Random.Range(sizeScale*-2.0f,sizeScale*2.0f);     
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
