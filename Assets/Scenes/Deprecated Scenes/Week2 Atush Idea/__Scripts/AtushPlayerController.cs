using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtushPlayerController : MonoBehaviour
{

    [SerializeField] private float maxJump;
    [SerializeField] private float minJump;
    private float moveInput;
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool canJump;

    
    [SerializeField] private float jumpValue = 0f;

    [SerializeField] private float currJump;
    
    [SerializeField] private float wallForce;

    [SerializeField] private int direction;

    [SerializeField] private Material defaultMaterial;

    [SerializeField] private List<GameObject> currentCollisions = new List<GameObject>();
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        defaultMaterial = GetComponent<Renderer>().material;

    }
    // Update is called once per frame
    void Update()
    {   

        moveInput = Input.GetAxisRaw("Horizontal");
        
        if(isGrounded){
            if(Input.GetKey(KeyCode.A)){
                direction = 1;
            }
            if(Input.GetKey(KeyCode.D)){
                direction = -1;
            }
            rb.velocity = new Vector2(moveSpeed * moveInput, rb.velocity.y);
        }
        if(Input.GetKey("space") && isGrounded && canJump && jumpValue <= maxJump){
            jumpValue += 20f * Time.deltaTime;
            GetComponent<Renderer>().material.color = Color.blue;
        }
        //if(!Input.GetKey("space") && isGrounded && canJump ){
        //    rb.velocity = new Vector2(moveSpeed * moveInput, rb.velocity.y);
        //}

        if(Input.GetKeyUp("space")){
            GetComponent<Renderer>().material.color = Color.red;
            if(jumpValue < minJump){
                jumpValue = minJump;
            }
            if(jumpValue > maxJump){
                jumpValue = maxJump;
            }
            if(isGrounded){
                currJump = jumpValue;
                rb.velocity = new Vector2(-direction * moveSpeed, jumpValue);
                jumpValue = 0f;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7 && !(collision.gameObject.layer == 6)){
            isGrounded = true;
            Invoke("Resetjump", 0.2f);
        }
        else{
            if(collision.gameObject.name == "Wall Left"){
                rb.AddForce(Vector2.right * wallForce, ForceMode2D.Impulse); 
                //rb.velocity = new Vector2((-direction * moveSpeed), rb.velocity.y);
            }
            if(collision.gameObject.name == "Wall Right"){
                rb.AddForce(Vector2.left * wallForce, ForceMode2D.Impulse); 
            }
        }
    }
    
    void OnDrawGizmos() {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawCube(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 0.3f),new Vector2(0.9f, 0.2f));    
    }

    void Resetjump(){
        canJump = true;
        jumpValue = 0;
    }

    void OnCollisionExit2D(Collision2D other) {
        isGrounded = false;
        canJump = false;
    }
}
