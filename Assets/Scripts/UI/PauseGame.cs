using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public void pauseGame ()
    {
        Time.timeScale = 0;
    }

	public void resumeGame ()
    {
        Time.timeScale = 1;
    }

}
