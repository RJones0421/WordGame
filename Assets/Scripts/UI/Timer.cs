using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{

	Image timerBar;

    [SerializeField]
	private float maxTime;

	public float timeLeft;
	public GameObject winCanvas;
    public GameObject canvasGroup;
    private bool timerRunning = false;

    public LossText lossText;

    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;

    public Word word;

    public SoundEffectSO gameEndSound;
    public SoundEffectSO timeLowSound;
    private bool isTimeLowSoundPlaying;

    // Start is called before the first frame update
    void Start()
    {
    	winCanvas.SetActive(false);
        Shop_Purchase.resetShopPowerUpUI();
    	timerBar = GetComponent<Image>();
    	timeLeft = maxTime;
        Time.timeScale = 1;
        canvasGroup.GetComponent<CanvasGroup>().interactable = true;
        canvasGroup.GetComponent<CanvasGroup>().blocksRaycasts = true;
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        word = GameObject.Find("Word").GetComponent<Word>();
    }

    // Update is called once per frame
    void Update()
    {

        try{
            if (!timerRunning) return;
            if (timeLeft > 0) {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / maxTime;

                if (timeLeft <= 10 && !isTimeLowSoundPlaying)
                {
                    timeLowSound.Play();
                    isTimeLowSoundPlaying = true;
                }

                else if (timeLeft > 10 && isTimeLowSoundPlaying)
                {
                    timeLowSound.Stop();
                    isTimeLowSoundPlaying = false;
                }

            }
            else {
                if(Time.timeScale==1)
                {
                    foreach (AudioSource source in FindObjectsOfType<AudioSource>() as AudioSource[])
                    {
                        source.Stop();
                    }
                    gameEndSound.Play();

                    int score = SetValues();
                    Word word = GameObject.Find("Word").GetComponent<Word>();

                    lossText.SetLossText(false);
#if true
                    analyticsManagerScript.HandleEvent("death", new List<object>
                    {
                        "time",
                        Time.timeSinceLevelLoadAsDouble,
                        score,
                        word.validWordCount,
                        word.totalSubmissions,
                        word.totalWordLength,
                        word.totalValidWordLength,
                    });
#else
                    analyticsManagerScript.HandleEvent("death", new Dictionary<string, object>
                    {
                        { "cause", "time", },
                        { "time", Time.timeSinceLevelLoadAsDouble, },
                        { "userScore", score, },
                        { "validWordCount", word.validCount, },
                        { "totalSubmissions", word.totalSubmissions, },
                        { "totalWordLength", word.totalLength, },
                        { "totalValidWordLength",word.totalValidLength, },
                    });
#endif
                }
            }
        } catch(Exception e){
                Debug.Log("Exception occurred in Timer class's Update method: "
        +e.Message);
        }

    }

    public void pauseTimeActivated() {
        timerBar.color = Color.blue;
    }

    public void pauseTimeDeactivated() {
        timerBar.color = Color.white;
    }

    public int SetValues()
    {
        Anagram.reset();
        Shop_Purchase.resetShopPowerUpUI();
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
            //If unable to parse final score, then consider score to be 0 and log the error
            finalScore = 0;
            Debug.Log("Unable to parse final score after game completion, considering 0 as final score");
        }

        // testing for score multiplier
        // int new_score = ScoreMultiplier.DoubleScore(finalScore);
        // finalScore = new_score;
        // Debug.Log("Current final score post Double Score " + new_score);

        int highScore = ScoreUtils.updateAndGetHighsScore(finalScore);

        CurrencyUtils.addCurrency(finalScore);





        CurrencyUtils.displayCurrency("Currency_test");
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

        return finalScore;
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

    public bool isTimerRunning()
    {
        return timerRunning;
    }

}
