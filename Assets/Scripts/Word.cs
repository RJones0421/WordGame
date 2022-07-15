using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;

public class Word : MonoBehaviour
{

    [SerializeField] private WordEvaluator evaluator;

    [SerializeField] private Sprite defaultSprite;

    [SerializeField] public List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private SpriteRenderer leftSidebar;
    private SpriteRenderer rightSidebar;

    public List<LetterClass> letters = new List<LetterClass>();

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

        leftSidebar.color = Color.white;
        rightSidebar.color = Color.white;
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
                leftSidebar.color = new Color(188f/255f, 223f/255f, 138f/255f);
                rightSidebar.color = new Color(188f/255f, 223f/255f, 138f/255f);

                if (!hasSubmitOnce)
                {
                    arrows.SetActive(true);
                    arrows.GetComponent<ArrowController>().RecolorArrows(new Color(188f/255f, 223f/255f, 138f/255f));
                }
            }
            else {
                leftSidebar.color = new Color(237f/255f, 119f/255f, 119f/255f);
                rightSidebar.color = new Color(237f/255f, 119f/255f, 119f/255f);

                if (!hasClearedOnce && word.Length > 2)
                {
                    arrows.SetActive(true);
                    arrows.GetComponent<ArrowController>().RecolorArrows(new Color(237f/255f, 119f/255f, 119f/255f));
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

        while(leftWall.transform.localScale.y < 3 * wallScale.y) {
            leftWall.transform.localScale = new Vector3(wallScale.x, leftWall.transform.localScale.y + bounceRate * Time.deltaTime, wallScale.z);
            rightWall.transform.localScale = new Vector3(wallScale.x, rightWall.transform.localScale.y + bounceRate * Time.deltaTime, wallScale.z);
            yield return null;
        }

        while(leftWall.transform.localScale.y > wallScale.y) {
            leftWall.transform.localScale = new Vector3(wallScale.x, leftWall.transform.localScale.y - bounceRate * Time.deltaTime, wallScale.z);
            rightWall.transform.localScale = new Vector3(wallScale.x, rightWall.transform.localScale.y - bounceRate * Time.deltaTime, wallScale.z);
            yield return null;
        }

        if (leftWall.transform.localScale.y < wallScale.y)
        {
            leftWall.transform.localScale = wallScale;
            rightWall.transform.localScale = wallScale;
        }

        isCoroutineRunning = false;
    }

    public int submitWord(string wallSide) {
        // Check validity and get word score
        // If valid, clear list

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
        setMultiplier(1);

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

        leftSidebar.color = Color.white;
        rightSidebar.color = Color.white;
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
        StartCoroutine(sidebarBounce(15f));

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
}
