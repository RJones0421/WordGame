using UnityEngine;
using System.Collections;

public class StopTime : MonoBehaviour
{
    private static bool activated = false;
    // private Timer timer = transform.GetComponent<PlayerController>().timer;


    public static void reset()
    {
        activated = false;
    }

    public static IEnumerator Activate()
    {
        // timer = transform.GetComponent<PlayerController>().timer;
        // timer.StopTimer();

        float timePassed = 0;
        while (timePassed < 10)
        {
            timePassed += Time.deltaTime;

            yield return null;
        }

        // timer.StartTimer();
    }

    // input score will be doubled only if the user has activated the shop power up
    public static int DoubleScore(int finalScore)
    {
        Debug.Log("preFinal Score " + finalScore);

       if (activated)
       {
           int new_score = finalScore * 2;
           reset();

            Debug.Log("post final Score " + new_score);
           return new_score;
       }
       else
       {
           reset();
           return finalScore;
       }
    }
}
