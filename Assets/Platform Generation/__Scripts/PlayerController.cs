using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float halfWidth;
    private float wallRotate = 90.0f;

    public bool allowMouseMovement;

    private Vector2 down;
    private Rigidbody2D rb;

    public GameObject wallPrefab;
    public List<GameObject> walls;
    private PlatformEffector2D effector;

    private bool mouseOnScreen {
        get {
            return Input.mousePosition.x >= 0.0f && Input.mousePosition.x <= Screen.width &&
                Input.mousePosition.y >= 0.0f && Input.mousePosition.y <= Screen.height;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        down = Vector2.down * 2;
        rb = GetComponent<Rigidbody2D>();

        walls.Add(Instantiate(wallPrefab, Vector3.left * halfWidth, Quaternion.identity));
        walls[0].transform.Rotate(Vector3.back * wallRotate);

        walls.Add(Instantiate(wallPrefab, Vector3.right * halfWidth, Quaternion.identity));
        walls[1].transform.Rotate(Vector3.forward, wallRotate);
    }

    // Update is called once per frame
    void Update()
    {
        // Maintain constant downward velocity to replace gravity
        rb.velocity = down;

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

       // Debug.LogFormat("X INPUT: {0}", inputX);

        // Only allow upward input
        if (inputY < 0.0f)
        {
            inputY = 0.0f;
        }

        Vector3 movement = new Vector3(inputX, inputY, 0);
        movement *= Time.deltaTime * movementSpeed;

        transform.Translate(movement);

        if (allowMouseMovement && mouseOnScreen)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f);
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
        LetterPlatform platform = collision.gameObject.GetComponent<LetterPlatform>();
        effector = collision.gameObject.GetComponent<PlatformEffector2D>();
        if (platform != null)
        {
            if(effector != null){
                Debug.Log("hit effector");
            }
            else{
               // Debug.Log("didn't work");
            }
            platform.DarkenSprite();
        }
    }
}
