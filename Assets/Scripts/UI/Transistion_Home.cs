using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transistion_Home : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void transistionHome()
    {
        //unhide other gameobjects
        ScoreUtils.hideGameObjects(true);
        SceneManager.LoadScene("MainMenu");
    }

}