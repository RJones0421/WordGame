using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transistion_Home : MonoBehaviour
{
    public void transistionHome()
    {
        //unhide other gameobjects
        ScoreUtils.unhideGameObjects(true);
        SceneManager.LoadScene("MainMenu");
    }

}