using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float keyMovementSpeed;
    public float mouseMovementSpeed;
    private bool started = false;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Camera mainCamera;

    private float halfWidth;

    public bool allowMouseMovement;

    private Word word;
    
    private float wallRotate = 90.0f;
    public GameObject wallPrefab;
    public List<GameObject> walls;

    public GameObject gameOverCanvas;
    public Timer timer;
    public GameObject tempTutroial;

    public GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private float playerHeight;

    private bool bounceBackToCenter;
    private Vector3 bounceBackTargetPos;
    private float bounceBackSpeed = 50f;
    private float originalBounceBackSpeed = 50f;

    private bool MouseOnScreen
    {
        get
        {
            return Input.mousePosition.x >= 0.0f && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0.0f && Input.mousePosition.y <= Screen.height;
        }
    }

    private void Awake()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.SetActive(false);
        halfWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - wallPrefab.GetComponent<Renderer>().bounds.size.y / 2;

        walls.Add(Instantiate(wallPrefab, Vector3.left * halfWidth, Quaternion.identity));
        walls[0].transform.Rotate(Vector3.back * wallRotate);

        walls.Add(Instantiate(wallPrefab, Vector3.right * halfWidth, Quaternion.identity));
        walls[1].transform.Rotate(Vector3.forward, wallRotate);

        word.SetSidebars(walls);

        halfWidth -= wallPrefab.GetComponent<Renderer>().bounds.size.y / 2 + GetComponent<BoxCollider2D>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle Mouse Movement
        if (Input.GetKeyDown(KeyCode.M))
        {
            allowMouseMovement = !allowMouseMovement;
        }

        // Jump
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!started)
                {
                    tempTutroial.SetActive(false);
                    timer.StartTimer();
                    rb.velocity = new Vector2(rb.velocity.x, 10.0f);
                    box.isTrigger = true;
                    started = true;
                }
            }
        }

        // Horizontal controls
        {
            // Keys
            if (!allowMouseMovement)
            {
                float inputX = Input.GetAxis("Horizontal");

                Vector3 movement = new Vector3(inputX, 0, 0);
                movement *= Time.deltaTime * keyMovementSpeed;

                transform.Translate(movement);
            }

            // Mouse
            if (allowMouseMovement)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f), Time.deltaTime * mouseMovementSpeed);
            }
        }

        // Word submission
        {
            if (transform.position.x > halfWidth)
            {
                //transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
                bounceBackToCenter = true;
                bounceBackTargetPos = new Vector3(0, transform.position.y + 10, 0);

                Debug.Log("SUBMIT RIGHT");

                word.submitWord();
            }

            if (transform.position.x < -halfWidth)
            {
                //transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
                bounceBackToCenter = true;
                bounceBackTargetPos = new Vector3(0, transform.position.y + 10f, 0);

                Debug.Log("SUBMIT LEFT");

                word.submitWord();
            }
        }

        if(bounceBackToCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, bounceBackTargetPos, Time.deltaTime * bounceBackSpeed);

            bounceBackSpeed *= 0.98f;

            if (bounceBackSpeed < 1)
            {
                bounceBackToCenter = false;
                bounceBackSpeed = originalBounceBackSpeed;
            }
        }

        {
            if (transform.position.y > playerHeight)
            {
                scoreManagerScript.AddScore(1);
                playerHeight += 2.5f;
            }
        }

        // Camera and walls follow as long as you go up
        float currHeight = transform.position.y;
        {
            float camHeight = mainCamera.transform.position.y;
            if (camHeight < currHeight)
            {
                mainCamera.transform.position = new Vector3(0.0f, currHeight, -1.0f);
                walls[0].transform.position = new Vector3(walls[0].transform.position.x, currHeight, 0.0f);
                walls[1].transform.position = new Vector3(walls[1].transform.position.x, currHeight, 0.0f);
            }
        }

        // Handle death
        {
            float screenPos = mainCamera.WorldToScreenPoint(new Vector3(0.0f, currHeight - GetComponent<Renderer>().bounds.size.y / 2, 0.0f)).y;
            if (screenPos < 0.0f)
            {
                Debug.Log("YOU DIED");

                gameOverCanvas.SetActive(true);
                timer.StopTimer();
                timer.SetValues();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (started && rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 10.0f);
            LetterPlatform platform;
            if (platform = collision.GetComponent<LetterPlatform>())
            {
                platform.CollectLetter();
            }
        }
    }
}