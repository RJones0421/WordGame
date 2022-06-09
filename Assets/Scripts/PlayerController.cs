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

    private Word word;

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

        word = GameObject.Find("Word").GetComponent<Word>();

        halfWidth = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - wallPrefab.GetComponent<Renderer>().bounds.size.y / 2;
        rb = GetComponent<Rigidbody2D>();

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
            walls[0].transform.position = new Vector3(walls[0].transform.position.x, currHeight, 0.0f);
            walls[1].transform.position = new Vector3(walls[1].transform.position.x, currHeight, 0.0f);
        }

        // Handle death
        float screenPos = Camera.main.WorldToScreenPoint(new Vector3(0.0f, currHeight - GetComponent<Renderer>().bounds.size.y / 2, 0.0f)).y;
        if (screenPos < 0.0f)
        {
            Debug.Log("YOU DIED");

            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        air = false;

        if (rb.velocity.y == 0.0f)
        {
            LetterPlatform platform = collision.gameObject.GetComponent<LetterPlatform>();
            if (platform != null)
            {
                platform.CollectLetter();
            }
        }
    }
}
