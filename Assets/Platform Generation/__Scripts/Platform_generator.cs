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
    
    
    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Camera.main.orthographicSize;

        /* Initial generation of platforms */
        for (int i=0; i<platform_maxCount; i++){
            /* Position of the new platform */
            spawnPosition.y+=Random.Range(1.0f,1.5f);
            spawnPosition.x=Random.Range(-2.0f,2.0f);
            
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
            platform.transform.position.y >= 1.2*screenHeight){

            // Debug.Log("Updating platform");        

            /* update the position to be the highest platform */
            spawnPosition.y+=Random.Range(1.0f,1.5f);
            spawnPosition.x=Random.Range(-2.0f,2.0f);     
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
