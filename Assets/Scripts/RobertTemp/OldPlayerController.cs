using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldPlayerController : MonoBehaviour
{
    private bool started = false;
    private Rigidbody2D rb;
    // private MeshCollider2D mc;
    private BoxCollider2D box;
    private PlatformEffector2D effector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        effector = GetComponent<PlatformEffector2D>();
        // mc = GetComponent<MeshCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(10 * inputX, 0, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        Camera.main.transform.position = new Vector3(0, transform.position.y, Camera.main.transform.position.z);


        if (Input.GetButtonDown("Jump"))
        {
            if (!started)
            {
                rb.velocity = new Vector2(rb.velocity.x, 10.0f);
                box.isTrigger = true;
                started = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (started) rb.velocity = new Vector2(rb.velocity.x, 10.0f);


        /* Darken the color of the sprite.
         * This should probably be moved somewhere else when we change the condition for collecting a letter */
        collision.gameObject.GetComponent<LetterPlatform>().DarkenSprite();

    }
}
