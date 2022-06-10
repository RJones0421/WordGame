using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word : MonoBehaviour
{

    [SerializeField] private WordEvaluator evaluator;

    [SerializeField] private Sprite defaultSprite;

    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private SpriteRenderer leftSidebar;
    private SpriteRenderer rightSidebar;

    private List<LetterClass> letters = new List<LetterClass>();

    private int currentLetterBox = 0;


    public GameObject timer;

    private Timer timerClass;
    
    private string word = "";

    private void Awake()
    {
        timerClass = timer.GetComponent<Timer>();
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
        if (newLetter.Letter == '_') return false;
        if (letters.Count >= 8) return false;

        else
        {
            letters.Add(newLetter);
            word += newLetter.Letter;
            sprites[currentLetterBox].sprite = newLetter.LetterSprite;
            currentLetterBox++;

            // Update Sidebars
            bool valid = evaluator.IsValidWord(word);
            if (valid) {
                leftSidebar.color = Color.green;
                rightSidebar.color = Color.green;
            }
            else {
                leftSidebar.color = Color.red;
                rightSidebar.color = Color.red;
            }
        }

        return true;
    }

    private IEnumerator sidebarBounce(float bounceRate)
    {
        GameObject leftWall = leftSidebar.gameObject;
        GameObject rightWall = rightSidebar.gameObject;

        Vector3 wallScale = leftWall.transform.localScale;

        while(leftWall.transform.localScale.y < 3 * wallScale.y) {
            leftWall.transform.localScale = new Vector3(wallScale.x, leftWall.transform.localScale.y + bounceRate, wallScale.z);
            rightWall.transform.localScale = new Vector3(wallScale.x, rightWall.transform.localScale.y + bounceRate, wallScale.z);
            yield return null;
        }

        while(leftWall.transform.localScale.y > wallScale.y) {
            leftWall.transform.localScale = new Vector3(wallScale.x, leftWall.transform.localScale.y - bounceRate, wallScale.z);
            rightWall.transform.localScale = new Vector3(wallScale.x, rightWall.transform.localScale.y - bounceRate, wallScale.z);
            yield return null;
        }

        
    }

    public int submitWord() {
        // Check validity and get word score
        // If valid, clear list

        int score = evaluator.SubmitWord(word);

        letters.Clear();
        word = "";
        foreach (SpriteRenderer sr in sprites) sr.sprite = defaultSprite;
        currentLetterBox = 0;

        float timeGained = Mathf.Clamp(score / 50, 0, timerClass.GetMaxTime() - timerClass.GetTime());
        timerClass.AddTime(timeGained);
        Debug.Log("Time gained: " + timeGained);

        leftSidebar.color = Color.gray;
        rightSidebar.color = Color.gray;

        StartCoroutine(sidebarBounce(0.12f));

        return score;
    }
}
