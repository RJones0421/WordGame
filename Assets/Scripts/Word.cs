using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Word : MonoBehaviour
{

    [SerializeField] private WordEvaluator evaluator;

    [SerializeField] private Sprite defaultSprite;
    [SerializeField] LetterObjectArray letterArray;

    [SerializeField] public List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private SpriteRenderer leftSidebar;
    private SpriteRenderer rightSidebar;

    public List<LetterClass> letters = new List<LetterClass>();

    public List<Sprite> wallSprites = new List<Sprite>();

    public int currentLetterBox = 0;

    public GameObject timer;

    private Timer timerClass;

    public string word = "";

    public GameObject scoreManager;
    private ScoreManager scoreManagerScript;

    private bool isCoroutineRunning;

    public GameObject arrows;
    private bool hasSubmitOnce;
    private bool hasClearedOnce;

    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;

    public int validWordCount;
    public int totalSubmissions;
    public int totalWordLength;
    public int totalValidWordLength;

    public TMP_Text addScoreAmountLeft;
    public TMP_Text addScoreAmountRight;
    private TMP_Text addScoreAmount;

    private int multiplier = 1;

    public SoundEffectSO wordSubmitSound;
    public SoundEffectSO wordClearSound;

    private void Awake()
    {
        timerClass = timer.GetComponent<Timer>();
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();
        arrows.SetActive(false);

        validWordCount = 0;
        totalSubmissions = 0;
        totalWordLength = 0;
        totalValidWordLength = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) submitWord("");
    }

    public void SetSidebars(List<GameObject> walls)
    {
        leftSidebar = walls[0].GetComponent<SpriteRenderer>();
        rightSidebar = walls[1].GetComponent<SpriteRenderer>();

        leftSidebar.sprite = wallSprites[0];
        rightSidebar.sprite = wallSprites[0];
    }

    public bool addLetter(LetterClass newLetter)
    {
        arrows.SetActive(false);


        if (newLetter.Letter == '_') return false;
        if (newLetter.Letter == '?' && word.Contains('?')) return false;
        if (letters.Count >= 8) return false;

        letters.Add(newLetter);
        word += newLetter.Letter;
        sprites[currentLetterBox].sprite = newLetter.image;
        currentLetterBox++;

        // Update Sidebars
        UpdateSidebars();

        return true;
    }

    public void PopLetter() {

        if (word != "") {
            word = word.Substring(0, word.Length - 1);

            letters.RemoveAt(letters.Count - 1);

            currentLetterBox--;
            sprites[currentLetterBox].sprite = defaultSprite;
        }

        UpdateSidebars();

    }

    public void UpdateSidebars() {
        int wildcard = word.IndexOf('?');
        string tempWord = word;
        while (wildcard != -1) {
            bool wc = false;
            char c = 'A';
            StringBuilder sb = new StringBuilder(word);
            while (c <= 'Z' && !wc) {
                sb[wildcard] = c;
                if (evaluator.IsValidWord(sb.ToString())) {
                    wc = true;
                } else {
                    c++;
                }
            }
            tempWord = sb.ToString();
            wildcard = tempWord.IndexOf('?');
        }
        bool valid = evaluator.IsValidWord(tempWord);
            if (valid) {
                leftSidebar.sprite = wallSprites[1];
                rightSidebar.sprite = wallSprites[1];

                if (!hasSubmitOnce)
                {
                    arrows.SetActive(true);
                    arrows.GetComponent<ArrowController>().RecolorArrows(new Color(189f/255f, 223f/255f, 137f/255f));
                }
            }
            else {
                leftSidebar.sprite = wallSprites[2];
                rightSidebar.sprite = wallSprites[2];

                if (!hasClearedOnce && word.Length > 2)
                {
                    arrows.SetActive(true);
                    arrows.GetComponent<ArrowController>().RecolorArrows(new Color(232f/255f, 112f/255f, 96f/255f));
                }
            }
    }

    private IEnumerator sidebarBounce(float bounceRate)
    {
        if (isCoroutineRunning) yield break;
        isCoroutineRunning = true;

        GameObject leftWall = leftSidebar.gameObject;
        GameObject rightWall = rightSidebar.gameObject;

        Vector3 wallScale = leftWall.transform.localScale;

        while(leftWall.transform.localScale.x < 2.25f * wallScale.x) {
            leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x + bounceRate * Time.deltaTime, wallScale.y , wallScale.z);
            rightWall.transform.localScale = new Vector3(rightWall.transform.localScale.x + bounceRate * Time.deltaTime, wallScale.y , wallScale.z);
            yield return null;
        }

        while(leftWall.transform.localScale.x > wallScale.x) {
            leftWall.transform.localScale = new Vector3(leftWall.transform.localScale.x - bounceRate * Time.deltaTime, wallScale.y , wallScale.z);
            rightWall.transform.localScale = new Vector3(rightWall.transform.localScale.x - bounceRate * Time.deltaTime, wallScale.y , wallScale.z);
            yield return null;
        }

        if (leftWall.transform.localScale.x < wallScale.x)
        {
            leftWall.transform.localScale = wallScale;
            rightWall.transform.localScale = wallScale;
        }

        isCoroutineRunning = false;
    }

    public int submitWord(string wallSide) {
        // Check validity and get word score
        // If valid, clear list
        // if(Anagram.isActivated()){
        //     Anagram.reset();
        //     Shop_Purchase.deactivatePowerUpUI("Anagram");
        //     Debug.Log("Anagram reset");
        // }
        totalSubmissions++;
        totalWordLength += word.Length;

        arrows.SetActive(false);

        int wildcard = word.IndexOf('?');
        while (wildcard != -1) {
            char bestChar = 'A';
            int highScore = 0;
            for (char c = 'A'; c <= 'Z'; c++) {
                StringBuilder tempSB = new StringBuilder(word);
                tempSB[wildcard] = c;
                int tempScore = evaluator.SubmitWord(tempSB.ToString());
                if (tempScore > highScore) {
                    bestChar = c;
                    highScore = tempScore;
                }
            }
            StringBuilder sb = new StringBuilder(word);
            sb[wildcard] = bestChar;
            word = sb.ToString();
            wildcard = word.IndexOf('?');
        }

        int score = evaluator.SubmitWord(word) * multiplier;
        // delayed use of score multiplier power up, only deduct when the word is actually submitted
        if (multiplier > 1 && score > 0)
        {
            CurrencyUtils.useShopItem("3");
            CurrencyUtils.displayQuantityDynamic("3","Text_ScoreMultiplier_Qty","x: ");
            setMultiplier(1);
            Shop_Purchase.deactivatePowerUpUI("ScoreMultiplier");
        }

        if(Anagram.isActivated() && score > 0){
            Anagram.reset();
            CurrencyUtils.useShopItem("4");
            Shop_Purchase.deactivatePowerUpUI("Anagram");
            Debug.Log("Anagram reset");
            CurrencyUtils.displayQuantityDynamic("4","Text_Anagram_Qty","x: ");
        }

        scoreManagerScript.AddScore(score);
        if (score > 0)
        {
            wordSubmitSound.pitchRange = new Vector2(0.8f + 1/GetWordLength(), 0.8f + 1/GetWordLength());
            wordSubmitSound.Play(null, 0.1f);

            ScoreUtils.addWordToCollection(word, score);
            hasSubmitOnce = true;

            validWordCount++;
            totalValidWordLength += word.Length;
        }

        else
        {
            wordClearSound.Play(null, 0.15f);
            if (word.Length > 3)
            {
                hasClearedOnce = true;
            }
        }

#if true
        analyticsManagerScript.HandleEvent("wordSubmitted", new List<object>
        {
            Time.timeSinceLevelLoadAsDouble,
            score > 0,
            word,
            word.Length,
            score,
        });
#else
        analyticsManagerScript.HandleEvent("wordSubmitted", new Dictionary<string, object>
        {
            { "time", Time.timeSinceLevelLoadAsDouble, },
            { "validWord", score > 0, },
            { "word", word, },
            { "wordLength", word.Length, },
            { "wordScore", score, },
        });
#endif

        letters.Clear();
        word = "";
        foreach (SpriteRenderer sr in sprites) sr.sprite = defaultSprite;
        currentLetterBox = 0;

        float timeGained = Mathf.Clamp(score / 50, 0, timerClass.GetMaxTime() - timerClass.GetTime());
        timerClass.AddTime(timeGained);
        Debug.Log("Time gained: " + timeGained);
        Debug.Log("Word score: " + score);

        leftSidebar.sprite = wallSprites[0];
        rightSidebar.sprite = wallSprites[0];
        if(score != 0){
            if(wallSide == "left"){
                addScoreAmount = addScoreAmountLeft;
            }
            else{
                addScoreAmount = addScoreAmountRight;
            }
            addScoreAmount.text = "+" + score.ToString();
            addScoreAmount.alpha = 1;
            StartCoroutine(Fade());
        }
        StartCoroutine(sidebarBounce(7f));

        return score;
    }
    IEnumerator Fade()
    {
        while(addScoreAmount.alpha >= 0f )
        {
            addScoreAmount.alpha -= 0.1f;
            yield return new WaitForSeconds(.1f);
        }
    }

    public int GetWordLength()
    {
        return word.Length;
    }

    public LetterClass getLetter(int index)
    {
        return letters[index];
    }

    public void setMultiplier(int multi)
    {
        multiplier = multi;
    }
    
    public void setWord(string newWord){
        word = newWord;
    }
    public string getWord(){
        return word;
    }
    public void changeWord(string newWord){
        word = newWord;
        string newWordLower = newWord.ToLower();
        letters.Clear();
        currentLetterBox = 0;
        foreach(char c in newWordLower){
            Debug.Log("char of anagram is " + c);
            int index = c - 'a' + 1;
            Debug.Log(index);
            LetterClass newLetter = letterArray.GetLetter(c - 'a' + 1);
            sprites[currentLetterBox].sprite = newLetter.image;
            currentLetterBox++;
        }
    }

    public int getMultiplier()
    {
        return multiplier;
    }
}
