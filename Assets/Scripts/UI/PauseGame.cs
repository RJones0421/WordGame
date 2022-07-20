using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame ()
    {
        Time.timeScale = 0;
        GameObject inputFieldGo = GameObject.Find("PowerUpManager");
        inputFieldGo.SetActive(false);
    }

	public void resumeGame ()
    {
        Time.timeScale = 1;
        GameObject inputFieldGo = GameObject.Find("PowerUpManager");
        inputFieldGo.SetActive(true);
    }

}
