using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExtraBlanks : MonoBehaviour
{
    private int count = 0;
    
    public GameObject platformPrefab;
    public LetterClass blank;

    /* total number of platforms */
    public int platformMaxCount=20;

    /* all platforms are stored in queue */
    private Queue<GameObject> platformQueue = new Queue<GameObject>();
    GameObject bottomPlatform;

    [SerializeField]
    private Vector3 spawnPosition;

    private Vector2 size = new Vector2(2.0f, 4.0f);

    private Camera mainCamera;

    private bool drawing = false;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        /* Initial generation of platforms */
        for (int i=0; i<platformMaxCount; i++){
            /* Position of the new platform */

            /* Generate a new platform */
            GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);  // Quaternion.identity means no rotation
            
            /* update letter value */
            Platform platform = newPlatform.GetComponent<Platform>();
            LetterClass letterObject = blank;
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

        Debug.Log(transform.localScale);
    }

    private void MovePlatform()
    {
        spawnPosition.y=transform.position.y + Random.Range(-1.0f,transform.localScale.y/2-0.25f);
        spawnPosition.x=Random.Range(-transform.localScale.x/2+0.5f,transform.localScale.x/2-0.5f);

        Collider2D hit;

        int iters = 0;
        while(hit = Physics2D.OverlapBox(spawnPosition, new Vector2(2.0f,4.0f), 0, (1 << 7)))
        {
            drawing = true;
            spawnPosition.y=transform.position.y + Random.Range(-1.0f,transform.localScale.y/2-0.25f);
            spawnPosition.x=Random.Range(-transform.localScale.x/2+0.5f,transform.localScale.x/2-0.5f);
            print(spawnPosition);
            iters++;
            if (iters > 5)
            {
                print("STOPPED");
                break;
            }
        }
        drawing = true;
        bottomPlatform.transform.position=spawnPosition;

        /* update the platformQueue */
        platformQueue.Enqueue(bottomPlatform);
        bottomPlatform=platformQueue.Dequeue();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        count++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        count--;
        if (count < 3)
        {
            MovePlatform();
        }
    }

    private void OnDrawGizmos()
    {
        if (drawing)
            Gizmos.DrawWireCube(spawnPosition, size);
    }
}
