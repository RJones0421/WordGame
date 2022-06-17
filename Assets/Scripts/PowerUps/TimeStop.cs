using UnityEngine;
using System.Collections;

public class TimeStop : PowerUp
{
    private bool paused;
    private Timer timer;

    void Awake()
    {
        paused = false;
        timer = transform.GetComponent<PlayerController>().timer;
    }

    public override void Activate()
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
    }
}
