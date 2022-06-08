using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition_NewGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void newGame()
	{
        //unhide other gameobjects
        ScoreUtils.unhideGameObjects(true);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

}
