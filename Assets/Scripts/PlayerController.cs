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

    public bool allowMouseMovement;

    private Word word;

    private float wallDist;
    private float wallRotate = 90.0f;
    public GameObject wallPrefab;
    public List<GameObject> walls;

    public GameObject gameOverCanvas;
    public Timer timer;
    public GameObject tempTutroial;

    public GameObject wordBox;
    public GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private float playerHeight;

    private bool bounceBackToCenter;
    private Vector3 bounceBackTargetPos;
    private float bounceBackSpeed = 15f;
    private float originalBounceBackSpeed = 15f;
    private bool isBouncingBack;

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
        wallDist = mainCamera.ScreenToWorldPoint(new Vector3(0.0f, 770.0f, 0.0f)).y * 0.4f + wallPrefab.GetComponent<Renderer>().bounds.size.y;
        wallDist = 4.2f;

        walls.Add(Instantiate(wallPrefab, Vector3.left * wallDist, Quaternion.identity));
        walls[0].transform.Rotate(Vector3.back * wallRotate);

        walls.Add(Instantiate(wallPrefab, Vector3.right * wallDist, Quaternion.identity));
        walls[1].transform.Rotate(Vector3.forward, wallRotate);

        word.SetSidebars(walls);

        wallDist -= wallPrefab.GetComponent<Renderer>().bounds.size.y;
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
            if (!isBouncingBack && !allowMouseMovement)
            {
                float inputX = Input.GetAxis("Horizontal");

                Vector3 movement = new Vector3(inputX, 0, 0);
                movement *= Time.deltaTime * keyMovementSpeed;

                transform.Translate(movement);
            }

            // Mouse
            if (!isBouncingBack && allowMouseMovement)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f), Time.deltaTime * mouseMovementSpeed);
            }
        }

        // Word submission
        {
            if (transform.position.x > wallDist)
            {
                //transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
                bounceBackToCenter = true;
                bounceBackTargetPos = new Vector3(0, transform.position.y + 3f, 0);
                isBouncingBack = true;
                rb.gravityScale = 0;
                rb.velocity = Vector3.zero;

                Debug.Log("SUBMIT RIGHT");

                word.submitWord();
            }

            if (transform.position.x < -wallDist)
            {
                //transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
                bounceBackToCenter = true;
                bounceBackTargetPos = new Vector3(0, transform.position.y + 3f, 0);
                isBouncingBack = true;
                rb.gravityScale = 0;
                rb.velocity = Vector3.zero;

                Debug.Log("SUBMIT LEFT");

                word.submitWord();
            }
        }

        if(bounceBackToCenter)
        {
            transform.position = Vector3.MoveTowards(transform.position, bounceBackTargetPos, Time.deltaTime * bounceBackSpeed);

            if (Vector3.Distance(transform.position, bounceBackTargetPos) == 0)
            {
                isBouncingBack = false;
                bounceBackToCenter = false;
                bounceBackSpeed = originalBounceBackSpeed;
                rb.gravityScale = 1;
                rb.velocity = new Vector3(0, 5, 0);
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
            float screenPos = mainCamera.WorldToScreenPoint(new Vector3(0.0f, currHeight - GetComponent<Renderer>().bounds.size.y * 0.5f, 0.0f)).y;
            if (screenPos < 0.0f)
            {
                Debug.Log("YOU DIED");

                gameOverCanvas.SetActive(true);
                timer.StopTimer();
                timer.SetValues();
            }
        }

        // shop item activation
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                Debug.Log("Player clicked on 1");
                if (CurrencyUtils.useShopItem("1"))
                {
                    // activate shop item power up in this code block
                    Debug.Log("player uses item number 1");
                }
                else
                {
                    Debug.Log("player does not have item 1");
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                Debug.Log("Player clicked on 2");
                if (CurrencyUtils.useShopItem("1"))
                {
                    Debug.Log("player uses item number 2");
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                Debug.Log("Player clicked on 3");
                if (CurrencyUtils.useShopItem("1"))
                {
                    Debug.Log("player uses item number 3");
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                Debug.Log("Player clicked on 4");
                if (CurrencyUtils.useShopItem("1"))
                {
                    Debug.Log("player uses item number 4");
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                Debug.Log("Player clicked on 5");
                if (CurrencyUtils.useShopItem("5"))
                {
                    Debug.Log("player uses item number 1");
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                Debug.Log("Player clicked on 6");
                if (CurrencyUtils.useShopItem("6"))
                {
                    Debug.Log("player uses item number 1");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (started && rb.velocity.y < 0.0f)
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