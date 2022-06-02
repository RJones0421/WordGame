using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float halfWidth;
    private float wallRotate = 90.0f;
    private Vector2 down;
    private Rigidbody2D rb;

    public GameObject wallPrefab;
    public List<GameObject> walls;

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

        Vector2 mouse = Input.mousePosition;

        Debug.LogFormat("MOUSE: {0} {1}", mouse.x, mouse.y);

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

        Camera.main.transform.position = new Vector3(0.0f, transform.position.y, -1.0f);
        walls[0].transform.position = new Vector3(-halfWidth, transform.position.y, 0.0f);
        walls[1].transform.position = new Vector3(halfWidth, transform.position.y, 0.0f);
    }
}
