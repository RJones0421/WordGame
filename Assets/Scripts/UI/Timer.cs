using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

	Image timerBar;
	public float maxTime = 90f;
	float timeLeft;
	public GameObject winCanvas;
    public GameObject canvasGroup;

    // Start is called before the first frame update
    void Start()
    {

    	winCanvas.SetActive(false);
    	timerBar = GetComponent<Image>();
    	timeLeft = maxTime;
        Time.timeScale = 1;
        canvasGroup.GetComponent<CanvasGroup>().interactable = true;
        canvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        
    }

    // Update is called once per frame
    void Update()
    {

    	if (timeLeft > 0) {

    		timeLeft -= Time.deltaTime;
    		timerBar.fillAmount = timeLeft / maxTime;

    	}

    	else {

    		winCanvas.SetActive(true);
            Time.timeScale = 0;
            canvasGroup.GetComponent<CanvasGroup>().interactable = false;
            canvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;

    	}
        
    }
}
