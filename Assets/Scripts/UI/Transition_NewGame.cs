using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_NewGame : MonoBehaviour
{
	public void newGame()
	{
        //unhide other gameobjects
        ScoreUtils.unhideGameObjects(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
