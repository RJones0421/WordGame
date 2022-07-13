using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{

    [SerializeField] Slider controlsSlider;
    [SerializeField] Slider sensitivitySlider;

    void Awake() 
    {
        if(!PlayerPrefs.HasKey("controls")) PlayerPrefs.SetInt("controls", 0);
        else controlsSlider.value = PlayerPrefs.GetInt("controls");
        
        if(!PlayerPrefs.HasKey("sensitivity")) PlayerPrefs.SetFloat("sensitivity", 1.0f);
        else sensitivitySlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    public void SetControls(float setting) { PlayerPrefs.SetInt("controls", (int)setting); }

    public void SetSensitivity(float setting) { PlayerPrefs.SetFloat("sensitivity", setting); }
}
