using UnityEngine;
using System.Collections;

public class TimeStop : Powerup
{
    private bool paused;
    private Timer timer;

    void Awake()
    {
        paused = false;
        //timer = transform.GetComponent<PlayerController>().timer;
    }

    public override bool Activate()
    {
        return false;
        if (!paused)
        {
            timer.StopTimer();
            paused = true;
        }
        else
        {
            timer.StartTimer();
            Destroy(this);
        }
    }
}
