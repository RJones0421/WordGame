using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        timerClass = timer.GetComponent<Timer>();
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        arrows.SetActive(false); 
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) submitWord();
    }

    public void SetSidebars(List<GameObject> walls)
    {
        leftSidebar = walls[0].GetComponent<SpriteRenderer>();
        rightSidebar = walls[1].GetComponent<SpriteRenderer>();

        leftSidebar.color = Color.gray;
        rightSidebar.color = Color.gray;
    }

    public bool addLetter(LetterClass newLetter)
    {
        arrows.SetActive(false);

        if (newLetter.Letter == '_') return false;
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
        bool valid = evaluator.IsValidWord(word);
            if (valid) {
                leftSidebar.color = Color.green;
                rightSidebar.color = Color.green;

                if (!hasSubmitOnce)
                {
                    arrows.SetActive(true);
                    arrows.GetComponent<ArrowController>().RecolorArrows(Color.green);
                }
            }
            else {
                leftSidebar.color = Color.red;
                rightSidebar.color = Color.red;

                if (!hasClearedOnce && word.Length > 3)
                {
                    arrows.SetActive(true);
                    arrows.GetComponent<ArrowController>().RecolorArrows(Color.red);
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

    public int submitWord() {
        // Check validity and get word score
        // If valid, clear list

        arrows.SetActive(false);

        int score = evaluator.SubmitWord(word);

        scoreManagerScript.AddScore(score);

        if (score > 0)
        {
            ScoreUtils.addWordToCollection(word, score);
            hasSubmitOnce = true;
        }

        else if (word.Length > 3)
        {
            hasClearedOnce = true;
        }

        letters.Clear();
        word = "";
        foreach (SpriteRenderer sr in sprites) sr.sprite = defaultSprite;
        currentLetterBox = 0;

        float timeGained = Mathf.Clamp(score / 50, 0, timerClass.GetMaxTime() - timerClass.GetTime());
        timerClass.AddTime(timeGained);
        Debug.Log("Time gained: " + timeGained);

        leftSidebar.color = Color.gray;
        rightSidebar.color = Color.gray;

        StartCoroutine(sidebarBounce(15f));

        return score;
    }

    public int GetWordLength()
    {
        return word.Length;
    }
}
