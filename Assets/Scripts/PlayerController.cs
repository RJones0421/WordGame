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

        Vector3 movement = new Vector3(10 * inputX, 0, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        if (Input.GetButtonDown("Jump"))
        {
            if (!jump && !air)
            {
<<<<<<< HEAD
                rb.velocity = new Vector2(rb.velocity.x, 50.0f);
=======
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
>>>>>>> fec8a87b3c577b46bd50de52fa2cdc74e120e9ba
            }

            jump = true;
            air = true;
        }
        else
        {
            jump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        air = false;
    }
}
