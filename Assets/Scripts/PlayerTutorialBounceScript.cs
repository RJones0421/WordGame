using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialBounceScript : MonoBehaviour
{
    [SerializeField] private GameObject spaceTutorial;
    [SerializeField] private GameObject moveTutorial;
    [SerializeField] private GameObject[] sideBarTutorial;
    public PlayerTutorialMoveScript moveScript;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Camera mainCamera;
    private bool started;

    public bool collectedLetter;

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;
        started = false;
        collectedLetter = false;
    }

    void Update(){
        if(Input.GetButtonDown("Jump")){
            if (!started)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
                moveTutorial.SetActive(true);
                spaceTutorial.SetActive(false);
                box.isTrigger = true;
                started = true;
                moveScript.enabled = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10.0f);
            LetterPlatform platform;
            if (platform = collision.GetComponent<LetterPlatform>())
            {
                platform.CollectLetter();
                if(!collectedLetter){
                    collectedLetter = true;
                    sideBarTutorial[0].SetActive(true);
                    sideBarTutorial[1].SetActive(true);
                }
            }
        }
    }
}
