using UnityEngine;
using System.Collections;

public class ScoreMultiplier : MonoBehaviour
{
    private static bool activated = false;

    public static void reset()
    {
        activated = false;
    }

    public static void Activate()
    {
        activated = true;
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
