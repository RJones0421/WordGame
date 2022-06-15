using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTutorialMoveScript : MonoBehaviour
{
    [SerializeField] private GameObject moveTutorial;

    public List<GameObject> walls;

    public GameObject wallPrefab;

    private float wallRotate = 90.0f;
    private float halfWidth;

    public float keyMovementSpeed;

    private Camera mainCamera;


    public GameObject wordTutorial;

    private bool moveTutorialDone;

    public GameObject platformGenerator;

    public Word word;
    void Awake(){
        mainCamera = Camera.main;
    }
    

    void Start(){
        moveTutorialDone = false;
        halfWidth = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x - wallPrefab.GetComponent<Renderer>().bounds.size.y / 2;
    }
    
    void Update() {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ){

            moveTutorial.SetActive(false);
            if(!moveTutorialDone){
                moveTutorialDone = true;
                wordTutorial.SetActive(true);
                platformGenerator.SetActive(true);
                walls.Add(Instantiate(wallPrefab, Vector3.left * halfWidth, Quaternion.identity));
                walls[0].transform.Rotate(Vector3.back * wallRotate);

                walls.Add(Instantiate(wallPrefab, Vector3.right * halfWidth, Quaternion.identity));
                walls[1].transform.Rotate(Vector3.forward, wallRotate);
                word.SetSidebars(walls);
            }
        }
        float inputX = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(inputX, 0);
        movement *= Time.deltaTime * keyMovementSpeed;

        transform.Translate(movement);

        if (transform.position.x > halfWidth)
        {
            transform.position = new Vector3(0.0f, transform.position.y, 0.0f);

        }

        if (transform.position.x < -halfWidth)
        {
            transform.position = new Vector3(0.0f, transform.position.y, 0.0f);
        }

        float currHeight = transform.position.y;
        {
            float camHeight = mainCamera.transform.position.y;
            if (camHeight < currHeight)
            {
                mainCamera.transform.position = new Vector3(0.0f, currHeight, -1.0f);
                if(moveTutorialDone)
                {
                    walls[0].transform.position = new Vector3(walls[0].transform.position.x, currHeight, 0.0f);
                    walls[1].transform.position = new Vector3(walls[1].transform.position.x, currHeight, 0.0f);
                }
                
            }
        }
    }
}
