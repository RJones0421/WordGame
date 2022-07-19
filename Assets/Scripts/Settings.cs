using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] private Slider sensitivitySlider;

    void Awake() 
    {
        // Controls -- 0: Keyboard, 1: Mouse
        if(!PlayerPrefs.HasKey("controls")) PlayerPrefs.SetInt("controls", 0);

        if (!PlayerPrefs.HasKey("sensitivity")) PlayerPrefs.SetFloat("sensitivity", 1.0f);
        else sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    public void SetControls(int setting) { PlayerPrefs.SetInt("controls", setting); }

    public void SetSensitivity(float setting) { PlayerPrefs.SetFloat("sensitivity", setting); }
}
