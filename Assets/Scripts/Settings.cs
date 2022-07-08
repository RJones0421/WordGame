using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    // 0 - Keyboard, 1 - Mouse
    private int controls = 0;

    // Sensitivity 0.5 - 1.5, Default - 1.0
    private float sensitivity = 1.0f;

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public int GetControls() { return controls; }

    public void SetControls(float setting) { controls = (int)setting; }

    public float GetSensitvity() { return sensitivity; }

    public void SetSensitivity(float setting) { sensitivity = setting; }
}
