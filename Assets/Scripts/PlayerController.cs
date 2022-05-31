using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jump;
    private bool air;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        air = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = 0.0f;

        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        // Ground check
        if (v.y == 0.0f)
        {
            Debug.Log("ON GROUND");
            inputY = Input.GetAxis("Vertical");
        }

        Vector3 movement = new Vector3(10 * inputX, 100 * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        if (Input.GetButtonDown("Jump"))
        {
            if (!jump && !air)
            {
                rb.velocity = new Vector2(rb.velocity.x, 50.0f);
            }

            jump = true;
            air = true;

            Debug.Log("JUMP!!!");
            Debug.LogFormat("VELOCITY: {0} {1}", v.x, v.y);

            // Vector3 jump_movement = new Vector3(inputX, 5f, inputY);
            // jump_movement *= Time.deltaTime;

            // transform.Translate(jump_movement);

            //GetComponent<Rigidbody2D>().velocity = Vector2.up * 8;
        }
        else
        {
            jump = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        air = false;
    }
}
