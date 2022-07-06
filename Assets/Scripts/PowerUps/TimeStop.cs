using UnityEngine;
using System.Collections;

public class TimeStop : Powerup
{
    private bool paused;
    private Timer timer;

    void OnEnable()
    {
        paused = false;
        timer = GameObject.Find("Timer").GetComponent<Timer>();
    }
    
    public override bool Collect()
    {
        return Activate();
    }

    public override bool Activate()
    {
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

        return true;
    }
}
