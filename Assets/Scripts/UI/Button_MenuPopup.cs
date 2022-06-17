using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_MenuPopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Canvas canvas;
    public bool a = false;

    public void popup() {

    	if (a==false) {

    		a = true;
    		canvas.enabled = true;

    	}

    	else if (a==true) {

    		a = false;
    		canvas.enabled=false;

    	}

    }
}
