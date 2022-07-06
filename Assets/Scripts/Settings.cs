using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{

    // 0 - Keyboard, 1 - Mouse
    private int controls = 0;

    void Awake() 
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public int GetControls() { return controls; }

    public void SetControls(float setting) { controls = (int)setting; }
}
