using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(10 * inputX, 10 * inputY, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        if (Input.GetButtonDown("Jump"))
        {
        	// Vector3 jump_movement = new Vector3(inputX, 5f, inputY);
        	// jump_movement *= Time.deltaTime;

        	// transform.Translate(jump_movement);

        	GetComponent<Rigidbody2D>().velocity = Vector2.up * 8;
        }
    }
}
