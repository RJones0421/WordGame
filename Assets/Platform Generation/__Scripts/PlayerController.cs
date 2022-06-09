using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private float halfWidth;
    private float wallRotate = 90.0f;

    public bool allowMouseMovement;

    private bool jump;
    private bool air;

    private Rigidbody2D rb;

    public GameObject wallPrefab;
    public List<GameObject> walls;

    private bool MouseOnScreen {
        get {
            return Input.mousePosition.x >= 0.0f && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0.0f && Input.mousePosition.y <= Screen.height;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        air = true;
        halfWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - GetComponent<BoxCollider2D>().size.x / 2;
        rb = GetComponent<Rigidbody2D>();

        walls.Add(Instantiate(wallPrefab, Vector3.left * halfWidth, Quaternion.identity));
        walls[0].transform.Rotate(Vector3.back * wallRotate);

        walls.Add(Instantiate(wallPrefab, Vector3.right * halfWidth, Quaternion.identity));
        walls[1].transform.Rotate(Vector3.forward, wallRotate);
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(inputX, 0, 0);
        movement *= Time.deltaTime * movementSpeed;

        transform.Translate(movement);

        if (Input.GetButtonDown("Jump"))
        {
            if (!jump && !air)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
            }

            jump = true;
            air = true;
        }
        else
        {
            jump = false;
        }

        if (allowMouseMovement && MouseOnScreen)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f);
        }

        if (transform.position.x > halfWidth)
        {
            transform.position = new Vector3(0.0f, transform.position.y, 0.0f);

            Debug.Log("SUBMIT RIGHT");

            // Submit
            // Word::SubmitWord("word");
        }

        if (transform.position.x < -halfWidth)
        {
            transform.position = new Vector3(0.0f, transform.position.y, 0.0f);

            Debug.Log("SUBMIT LEFT");

            // Submit
            // Word::SubmitWord("word");
        }

        // Camera and walls follow as long as you go up
        float camHeight = Camera.main.transform.position.y;
        float currHeight = transform.position.y;
        if (camHeight < currHeight)
        {
            Camera.main.transform.position = new Vector3(0.0f, currHeight, -1.0f);
            walls[0].transform.position = new Vector3(-halfWidth, currHeight, 0.0f);
            walls[1].transform.position = new Vector3(halfWidth, currHeight, 0.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        air = false;

        LetterPlatform platform = collision.gameObject.GetComponent<LetterPlatform>();
        if (platform != null)
        {
            platform.DarkenSprite();
        }
    }
}
