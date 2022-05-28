using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_generator : MonoBehaviour
{
    public GameObject platformPrefab;
    
    /* total number of platforms */
    public int platform_maxCount=15;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        var platformQueue = new Queue<GameObject>();
        
        /* Initial generation of platforms */
        for (int i=0; i<platform_maxCount; i++){
            /* Position of the new platform */
            spawnPosition.y+=Random.Range(1.0f,1.5f);
            spawnPosition.x=Random.Range(-2.0f,2.0f);
            
            /* Generate a new platform */
            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation 
            platformQueue.Enqueue(platform);
        }
        
        /* Simulate the movement of camera */
        StartCoroutine(cameraMove()); // WaitForSeconds() requires to work inside StartCoroutine()
        IEnumerator cameraMove(){

            /* the lowest platform */
            GameObject platform = platformQueue.Dequeue();

            for (int t=0;t<1000;t++){
                
                /* height from center to top/bottom border */
                float screenHeight = Camera.main.orthographicSize;

                /* movement of camera */
                Camera.main.transform.position += new Vector3(0f, 0.3f, 0f);

                // Debug.Log(Camera.main.transform.position.y);
                // Debug.Log(platform.transform.position.y);
                // Debug.Log(screenHeight);

                /* track camera position */
                if (Camera.main.transform.position.y - 
                    platform.transform.position.y >= 1.2*screenHeight){

                    // Debug.Log("Updating platform");        

                    /* update the position to be the highest platform */
                    spawnPosition.y+=Random.Range(1.0f,1.5f);
                    spawnPosition.x=Random.Range(-2.0f,2.0f);     
                    platform.transform.position=spawnPosition;
                    
                    /* update the platformQueue */
                    platformQueue.Enqueue(platform);
                    platform=platformQueue.Dequeue();
                }

                /* Wait for 0.25 seconds */
                yield return new WaitForSecondsRealtime(0.25f);
            }   
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
