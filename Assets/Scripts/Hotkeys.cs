using UnityEngine.SceneManagement;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey("r")) {
            Restart();
        }
    }
 
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
