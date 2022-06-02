using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initialize_player_velocity : MonoBehaviour
{
    
    private Rigidbody2D rb;
    Vector3 player_velocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(player_velocity, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // player_velocity = rb.velocity;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
    	// testing for adding velocity to an object so it can reflect off a wall
    	// var speed = player_velocity.magnitude; 
    	// var direction = Vector3.Reflect(player_velocity.normalized, collision.contacts[0].normal); 
    	// rb.velocity = direction;
    }

}
