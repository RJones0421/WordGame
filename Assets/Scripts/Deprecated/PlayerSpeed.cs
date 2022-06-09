using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{
    private Rigidbody2D rb;
    // private MeshCollider2D mc;
    private BoxCollider2D box;
    private PlatformEffector2D effector;

    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private float boost = 2.0f;
    [SerializeField]
    private float boostTime = 0.5f;
    private float timeRemaining;
    private bool isBoosted = false;
    

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

        Vector3 movement = new Vector3(10 * inputX, speed, 0);
        movement *= Time.deltaTime;

        transform.Translate(movement);

        if (timeRemaining > 0.0f)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (isBoosted)
        {
            speed /= boost;
            isBoosted = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetBoost();
    }

    void SetBoost()
    {
        if (!isBoosted)
        {
            isBoosted = true;
            speed *= boost;
        }
        timeRemaining = boostTime;
    }
}
