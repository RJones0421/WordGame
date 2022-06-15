using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Timer : MonoBehaviour
{

	Image timerBar;
	public float maxTime = 90f;
	float timeLeft;
	public GameObject winCanvas;
    public GameObject canvasGroup;
    private bool timerRunning = false;

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

        try{
            if (!timerRunning) return;
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / maxTime;
            }
            else {
                if(Time.timeScale==1)
                {
                    SetValues();
                }
            }
        } catch(Exception e){
                Debug.Log("Exception occurred in Timer class's Update method: "
        +e.Message);
        }

    }

    public void SetValues()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0;
        canvasGroup.GetComponent<CanvasGroup>().interactable = false;
        canvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = false;
        GameObject inputFieldGo = GameObject.Find("CurrentScore");
        TMP_Text inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        int finalScore = 0;
        if (Int32.TryParse(inputFieldCo.text, out int j))
        {
            finalScore = j;
        }
        else
        {
            finalScore = UnityEngine.Random.Range(0, 999);
        }

        int highScore = ScoreUtils.updateAndGetHighsScore(finalScore);

        //set current score
        inputFieldGo = GameObject.Find("CurrentScore_Final");
        inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        inputFieldCo.text = "Your Score " + finalScore.ToString();

        //set high score
        inputFieldGo = GameObject.Find("Highscore_Final");
        inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        inputFieldCo.text = "High Score " + highScore.ToString();


        inputFieldGo = GameObject.Find("Result");
        inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        if (finalScore == highScore)
        {
            inputFieldCo.text = "You Win!";
        }
        else
        {
            inputFieldCo.text = "You Lose!";
        }

        //Display score breakdowns
        inputFieldGo = GameObject.Find("ScoreBreakdown");
        inputFieldCo = inputFieldGo.GetComponent<TMP_Text>();
        inputFieldCo.text = ScoreUtils.getTopKwordsCollected(5);

        //hide other gameobjects
        ScoreUtils.unhideGameObjects(false);

        //Clear collected words list
        ScoreUtils.clearCollectedWords();
    }

    public void AddTime(float amount)
    {
        timeLeft += amount;
    }

    public float GetTime()
    {
        return timeLeft;
    }

    public float GetMaxTime()
    {
        return maxTime;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

}
