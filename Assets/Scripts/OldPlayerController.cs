using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerController : MonoBehaviour
{
    public float movementSpeed;
    private bool started = false;
    private Rigidbody2D rb;
    // private MeshCollider2D mc;
    private BoxCollider2D box;
    private PlatformEffector2D effector;

    private float halfWidth;

    public bool allowMouseMovement;

    private Word word;

    private bool MouseOnScreen
    {
        get
        {
            return Input.mousePosition.x >= 0.0f && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0.0f && Input.mousePosition.y <= Screen.height;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
        halfWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - GetComponent<BoxCollider2D>().size.x / 2;
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        effector = GetComponent<PlatformEffector2D>();
        // mc = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(inputX, 0, 0);
        movement *= Time.deltaTime * movementSpeed;

        transform.Translate(movement);

        // Camera.main.transform.position = new Vector3(0, transform.position.y, Camera.main.transform.position.z);

        if (Input.GetButtonDown("Jump"))
        {
            if (!started)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
                box.isTrigger = true;
                started = true;
            }
        }

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

        // Camera and walls follow as long as you go up
        float camHeight = Camera.main.transform.position.y;
        float currHeight = transform.position.y;
        if (camHeight < currHeight)
        {
            Camera.main.transform.position = new Vector3(0.0f, currHeight, -1.0f);
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