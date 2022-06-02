using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool jump;
    private bool air;
    // used to represent the player (red circle itself)
    private Rigidbody2D rb;
    // private MeshCollider2D mc;
    private BoxCollider2D box;
    private PlatformEffector2D effector;

    Vector3 player_velocity;
    Vector3 initial_force;


	SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        jump = false;
        air = true;
        rb = GetComponent<Rigidbody2D>();
        // initialize the force vector of player 
        rb.AddForce(initial_force, ForceMode2D.Impulse);


        box = GetComponent<BoxCollider2D>();
        effector = GetComponent<PlatformEffector2D>();


        // sprite = GameObject.Find("Dead Zone") as SpriteRenderer;
        // mc = GetComponent<MeshCollider>();
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
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
            }

            jump = true;
            air = true;
        }
        else
        {
            jump = false;
        }

        player_velocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        air = false;
        Debug.Log ("hit + " + collision.gameObject.name);

        // changing the color of the borders on collisions 
        // creating new instance of a materal and attaching it to the border 
		var left_border =  GameObject.Find("Right Border");        
        var new_color = new Color (0, 1, 0, 0);

        Debug.Log("sprite name " + sprite.name);

        // calculating the players velocity to bounce create a new vector to bounce off border
        // var speed = player_velocity.magnitude;
        // var direction = Vector3.Reflect(player_velocity.normalized, new Vector3(0f, 7f, 0f));

        // rb.velocity = direction * speed;
    }
}
