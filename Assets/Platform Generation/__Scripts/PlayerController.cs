using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 down;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        down = Vector2.down * 2;
        rb = GetComponent<Rigidbody2D>();
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

        Vector3 movement = new Vector3(10 * inputX, 10 * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        Camera.main.transform.position = new Vector3(0.0f, transform.position.y, -1.0f);
    }
}
