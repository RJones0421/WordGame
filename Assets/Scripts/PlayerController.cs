using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private float halfWidth;

    public bool allowMouseMovement;

    private Vector2 down;
    private Rigidbody2D rb;

    private Word word;

    private bool MouseOnScreen {
        get {
            return Input.mousePosition.x >= 0.0f && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0.0f && Input.mousePosition.y <= Screen.height;
        }
    }

    private Vector2 originalPos;
    private Vector3 originalCameraPosition;

    private Vector3 players_start_position;
    Renderer m_Renderer;
    private bool spawned = false;
    private PlatformEffector2D effector;



    // Start is called before the first frame update
    void Start()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
        halfWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - GetComponent<BoxCollider2D>().size.x / 2;
        down = Vector2.down * 2;
        rb = GetComponent<Rigidbody2D>();
        effector = GetComponent<PlatformEffector2D>();
        // mc = GetComponent<MeshCollider>();
        players_start_position = rb.transform.position;
        m_Renderer = GetComponent<Renderer>();
        spawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Maintain constant downward velocity to replace gravity
        rb.velocity = down;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Only allow upward input
        if (inputY < 0.0f)
        {
            inputY = 0.0f;
        }

        Vector3 movement = new Vector3(inputX, inputY, 0);
        movement *= Time.deltaTime * movementSpeed;

        transform.Translate(movement);

        if (allowMouseMovement && MouseOnScreen)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f);
        }

        if (transform.position.x > halfWidth)
        {
            transform.position = new Vector3(0.0f, transform.position.y, 0.0f);

            Debug.Log("SUBMIT RIGHT");

            word.submitWord();
        }

        if (transform.position.x < -halfWidth)
        {
            transform.position = new Vector3(0.0f, transform.position.y, 0.0f);

            Debug.Log("SUBMIT LEFT");

            word.submitWord();
        }

        // checks if the player is visible on camera or not
        // if (!m_Renderer.isVisible && spawned)
        // {
        //     Debug.Log("player is NOT visible");
        //     // transform.position = players_start_position;
        //     // Camera.main.transform.position = players_start_position;
        // }

        // Camera and walls follow as long as you go up
        float camHeight = Camera.main.transform.position.y;
        float currHeight = transform.position.y;
        if (camHeight < currHeight)
        {
            Camera.main.transform.position = new Vector3(0.0f, currHeight, -1.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        LetterPlatform platform = collision.gameObject.GetComponent<LetterPlatform>();
        if (platform != null)
        {
            platform.DarkenSprite();
        }


        // Determines if player has gone past the camera and collided with the bottom border
        // In the event of a bottom border collision, all actions associated with a loss should go here
        // 1. Player loses a life 2. camera reset 3. player position reset
        if (collision.gameObject.name == "Lose Floor")
        {
        	Debug.LogFormat("LOSE, PLAYER HIT THE LOSE FLOOR");
            // resets the entire game state to the inital game state(also resets timer)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        Debug.LogFormat("HIT: {0}", collision.gameObject.name);

    }
}
