using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtushSecondPlayerController : MonoBehaviour
{

    private float moveInput;
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Material defaultMaterial;

    [SerializeField] private GameObject wordSelector;

    void Start() {
        defaultMaterial = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    void Update()
    {   

        moveInput = Input.GetAxisRaw("Horizontal");
        //if(!stopMove){
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime * moveInput, Space.World);
        //}
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.layer == LayerMask.NameToLayer("Platform")){
           //Destroy(collision.gameObject);
       }
    }

    /*void OnCollisionExit2D(Collision2D other) {
        collisionsLists.Remove(other.gameObject);
    }*/
}
