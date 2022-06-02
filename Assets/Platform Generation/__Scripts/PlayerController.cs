using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private bool jump;
    //private bool air;
    private Vector2 down;
    private Rigidbody2D rb;
    //private BoxCollider2D box;
    //private PlatformEffector2D effector;

    // Start is called before the first frame update
    void Start()
    {
        //jump = false;
        //air = true;
        down = Vector2.down * 2;
        rb = GetComponent<Rigidbody2D>();
        //box = GetComponent<BoxCollider2D>();
        //effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.position = new Vector3(0.0f, Camera.main.transform.position.y, -1.0f);
        rb.velocity = down;

        Debug.LogFormat("VELOCITY: {0} {1}", rb.velocity.x, rb.velocity.y);

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        if (inputY < 0.0f)
        {
            inputY = 0.0f;
        }

        Vector3 movement = new Vector3(10 * inputX, 10 * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);


        //if (Input.GetButtonDown("Jump"))
        //{
        //    if (!jump && !air)
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, 10.0f);
        //    }

        //    jump = true;
        //    air = true;
        //}
        //else
        //{
        //    jump = false;
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //air = false;
    }
}
