using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    void Awake() 
    {
        PlayerPrefs.SetInt("controls", 0);
        PlayerPrefs.SetFloat("sensitivity", 1.0f);
    }

    public void SetControls(float setting) { PlayerPrefs.SetInt("controls", (int)setting); }

    public void SetSensitivity(float setting) { PlayerPrefs.SetFloat("sensitivity", setting); }
}
