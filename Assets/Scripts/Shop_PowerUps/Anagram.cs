using UnityEngine;
using System.Collections;

public class Anagram : MonoBehaviour
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

    public static bool isActivated()
    {
        return activated;
    }
}
