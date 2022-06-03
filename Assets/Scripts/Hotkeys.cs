using UnityEngine.SceneManagement;
using UnityEngine;

public class Hotkeys : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            Restart();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SceneManager.LoadScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SceneManager.LoadScene(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            SceneManager.LoadScene(4);
        }
    }
 
    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
