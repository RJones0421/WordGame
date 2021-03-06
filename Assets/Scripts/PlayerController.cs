using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    public float keyMovementSpeed;
    public float mouseMovementSpeed;
    private bool started = false;
    private Rigidbody2D rb;
    private BoxCollider2D box;
    private Camera mainCamera;
    private Renderer renderer;


    public bool allowMouseMovement;

    public Word word;

    private float wallDist;
    private float wallRotate = 90.0f;
    public GameObject wallPrefab;
    public List<GameObject> walls;

    public ParticleSystem scoreParticles;
    public ParticleSystem leftParticles;
    public ParticleSystem rightParticles;
    // public ParticleSystem validWordParticles;
    // public ParticleSystem invalidWordParticles;
    public ParticleSystemForceField forceField;

    // private var rightMain;
    // private var rightExternal;
    // private var leftMain;
    // private var leftExternal;


    public GameObject gameOverCanvas;
    public Timer timer;
    public GameObject tempTutroial;

    public LossText lossText;

    public GameObject wordBox;
    public GameObject scoreManager;
    private ScoreManager scoreManagerScript;
    private float playerHeight;
    private Vector2 screenRes;

    // private Color greenChalk = new Color(189, 223, 137, 1F);
    // private Color redChalk = new Color(232, 112, 96, 1F);
    private Color greenChalk = new Color(0, 1, 0, 0.7F);
    private Color redChalk = new Color(1, 0, 0, 0.75F);

    private bool bounceBackToCenter;
    private Vector3 bounceBackTargetPos;
    private float bounceBackSpeed = 15f;
    private float originalBounceBackSpeed = 15f;
    private bool isBouncingBack;

    public GameObject analyticsManager;
    private AnalyticsManager analyticsManagerScript;

    public SoundEffectSO bounceSound;
    public SoundEffectSO letterCollectSound;
    public SoundEffectSO wallBounceSound;
    public SoundEffectSO gameEndSound;

    public Transform height;

    private TextMeshProUGUI controlsTutorial;

    public static int lives;
    private bool resurrect;

    public SpriteRenderer currSprite;
    public Sprite playerGround;
    public Sprite playerJump;
    public Sprite playerSubmit;

    public bool faceLeft;
    public bool onGround;
    public bool onWall;

    private void Awake()
    {
        word = GameObject.Find("Word").GetComponent<Word>();
        scoreManagerScript = scoreManager.GetComponent<ScoreManager>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
        renderer = GetComponent<Renderer>();
        mainCamera = Camera.main;
        analyticsManagerScript = analyticsManager.GetComponent<AnalyticsManager>();

        lives = 0;
        resurrect = false;

        // Get Tutorial Text
        controlsTutorial = tempTutroial.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        screenRes = new Vector2(Screen.width, Screen.height);
        gameOverCanvas.SetActive(false);
        wallDist = wordBox.GetComponent<SpriteRenderer>().bounds.size.x * 0.5f + wallPrefab.GetComponent<Renderer>().bounds.size.y * 1.5f;

        walls.Add(Instantiate(wallPrefab, Vector3.left * wallDist, Quaternion.identity));
        walls.Add(Instantiate(wallPrefab, Vector3.right * wallDist, Quaternion.identity));

        word.SetSidebars(walls);

        leftParticles = walls[0].GetComponent<ParticleSystem>();
        rightParticles = walls[1].GetComponent<ParticleSystem>();

        // rightParticles.externalForces.enabled = true;
        // leftParticles.externalForces.enabled = true;

        rightParticles.externalForces.AddInfluence(forceField);
        leftParticles.externalForces.AddInfluence(forceField);

        // rightMain = rightParticles.main;
        // rightExternal = rightParticles.externalForces;
        // leftMain = leftParticles.main;
        // leftExternal = leftParticles.externalForces;

        // intialize the shop item objects
        ScoreMultiplier.reset();

        if(PlayerPrefs.HasKey("controls")) {
            if(PlayerPrefs.GetInt("controls") == 0) {
                allowMouseMovement = false;
                controlsTutorial.text = "A/D to move";
            }
            else {
                allowMouseMovement = true;
                controlsTutorial.text = "Mouse to move";
            }
        }
        else {
            allowMouseMovement = false;
            controlsTutorial.text = "A/D to move";
        }

        // updating shop item quantity for the left hand panel
        UpdateShopItemCount();
    }

    // Update is called once per frame
    void Update()
    {
        // Resolution Changes
        if (Screen.width != screenRes.x || Screen.height != screenRes.y)
        {
            wallDist *= ((screenRes.y * (float)Screen.width) / (screenRes.x * (float)Screen.height));
            screenRes = new Vector2(Screen.width, Screen.height);
            walls[0].transform.position = new Vector3(wallDist, walls[0].transform.position.y, 0.0f);
            walls[1].transform.position = new Vector3(-wallDist, walls[1].transform.position.y, 0.0f);
        }

        // Jump
        {
            if (Input.GetButtonDown("Jump"))
            {
                if (!started)
                {
                    tempTutroial.SetActive(false);
                    timer.StartTimer();
                    rb.velocity = new Vector2(rb.velocity.x, 10.0f);
                    box.isTrigger = true;
                    started = true;

                    bounceSound.Play();

                }
            }
        }

        // Horizontal controls
        {
            // Keys
            if (!isBouncingBack && !allowMouseMovement)
            {
                float inputX = Input.GetAxis("Horizontal");

                Vector3 movement = new Vector3(inputX, 0, 0);
                movement *= Time.deltaTime * keyMovementSpeed;
                if (PlayerPrefs.HasKey("sensitivity")) movement *= PlayerPrefs.GetFloat("sensitivity");

                transform.Translate(movement);
            }

            // Mouse
            if (!isBouncingBack && allowMouseMovement)
            {
                if(PlayerPrefs.HasKey("sensitivity")) {
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f), Time.deltaTime * mouseMovementSpeed * PlayerPrefs.GetFloat("sensitivity"));
                }
                else transform.position = Vector3.MoveTowards(transform.position, new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, transform.position.y, 0.0f), Time.deltaTime * mouseMovementSpeed);
            }
        }

        // Word submission
        {
            if (transform.position.x > wallDist - wallPrefab.GetComponent<Renderer>().bounds.size.y)
            {
                if (started && word.GetWordLength() > 0)
                {
                    InitiateBounce("right");
                    var rightMain = rightParticles.main;
                    var rightExternal = rightParticles.externalForces;


                    Debug.Log("SUBMIT RIGHT");

                    if (word.submitWord("right") > 0)
                    {
                        rightMain.gravityModifier = 0;
                        rightExternal.enabled = true;
                        rightMain.startColor = greenChalk;
                        rightParticles.Play();
                        scoreParticles.Play();
                        // validWordParticles.Play();
                    }
                    else {
                        rightMain.gravityModifier = 5;
                        rightExternal.enabled = false;
                        rightMain.startColor = redChalk;
                        rightParticles.Play();
                        // invalidWordParticles.Play();
                    }
                }
                else transform.position = new Vector3(wallDist - wallPrefab.GetComponent<Renderer>().bounds.size.y, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -wallDist + wallPrefab.GetComponent<Renderer>().bounds.size.y)
            {
                if (started && word.GetWordLength() > 0)
                {
                    InitiateBounce("left");
                    var leftMain = leftParticles.main;
                    var leftExternal = leftParticles.externalForces;

                    Debug.Log("SUBMIT LEFT");

                    if (word.submitWord("left") > 0)
                    {
                        leftMain.gravityModifier = 0;
                        leftExternal.enabled = true;
                        leftMain.startColor = greenChalk;
                        leftParticles.Play();
                        scoreParticles.Play();
                        // validWordParticles.Play();
                    }
                    else {
                        leftMain.gravityModifier = 5;
                        leftExternal.enabled = false;
                        leftMain.startColor = redChalk;
                        leftParticles.Play();
                        // invalidWordParticles.Play();
                    }
                }
                else transform.position = new Vector3(-wallDist + wallPrefab.GetComponent<Renderer>().bounds.size.y, transform.position.y, transform.position.z);
            }
        }
            if (bounceBackToCenter)
            {
                transform.position = Vector3.MoveTowards(transform.position, bounceBackTargetPos, Time.deltaTime * bounceBackSpeed);

                if (Vector3.Distance(transform.position, bounceBackTargetPos) == 0)
                {
                    isBouncingBack = false;
                    bounceBackToCenter = false;
                    bounceBackSpeed = originalBounceBackSpeed;
                    rb.gravityScale = 1;
                    rb.velocity = new Vector3(0, 5, 0);
                }
            }

        {
            if (transform.position.y > playerHeight)
            {
                scoreManagerScript.AddScore(1);
                playerHeight += 2.5f;
            }
        }

        // Camera and walls follow as long as you go up
        float currHeight = transform.position.y - 1.0f;
        {
            float camHeight = mainCamera.transform.position.y;
            if (camHeight < currHeight)
            {
                mainCamera.transform.position = new Vector3(0.0f, currHeight, -1.0f);
                walls[0].transform.position = new Vector3(walls[0].transform.position.x, currHeight, 0.0f);
                walls[1].transform.position = new Vector3(walls[1].transform.position.x, currHeight, 0.0f);
            }
        }

        // Handle death
        {
            float screenPos = mainCamera.WorldToScreenPoint(new Vector3(0.0f, currHeight - renderer.bounds.size.y * 0.5f + 1.0f, 0.0f)).y;
            if (screenPos < 0.0f)
            {
                //Debug.Log("Lives: " + lives);
                //if (lives > 0)
                if (resurrect)
                {
                    //Debug.LogFormat("YOU DIED BUT HAD {0} LIVES REMAINING", lives--);

                    resurrect = false;
                    Shop_Purchase.deactivatePowerUpUI("ExtraLife");

                    CurrencyUtils.useShopItem("2");
                    CurrencyUtils.displayQuantityDynamic("2", "Text_ExtraLife_Qty", "x: ");

                    timer.StopTimer();
                    timer.timeLeft = timer.GetMaxTime();
                    timer.StartTimer();

                    transform.position = mainCamera.transform.position.x * Vector3.right + mainCamera.transform.position.y * Vector3.up;
                    rb.velocity = new Vector2(0,5);
                }
                else
                {
                    Debug.Log("YOU DIED");

                    gameOverCanvas.SetActive(true);
                    timer.StopTimer();
                    int score = timer.SetValues();

                    foreach (AudioSource source in FindObjectsOfType<AudioSource>() as AudioSource[])
                    {
                        source.Stop();
                    }
                    gameEndSound.Play();

#if true
                    analyticsManagerScript.HandleEvent("death", new List<object>
                    {
                        "falling",
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
                        { "cause", "falling", },
                        { "time", Time.timeSinceLevelLoadAsDouble, },
                        { "userScore", score, },
                        { "validWordCount", word.validCount, },
                        { "totalSubmissions", word.totalSubmissions, },
                        { "totalWordLength", word.totalLength, },
                        { "totalValidWordLength",word.totalValidLength, },
                    });
#endif
                }
                lossText.SetLossText(true);
            }
        }

        // shop item activation
        {
            // pause time
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
            {
                Debug.Log("Player clicked on 1");
                if(timer.isTimerRunning())
                {
                    if (CurrencyUtils.useShopItem("1"))
                    {
                        // activate shop item power up in this code block
                        Debug.Log("player uses item number 1 - Stop Time");
                        StartCoroutine(StopTime(1.0f, 5.0f));
                        Shop_Purchase.activatePowerUpUI("PauseTime");
                        CurrencyUtils.displayQuantityDynamic("1","Text_PauseTime_Qty","x: ");
                    }
                    else
                    {
                        Debug.Log("player does not have item 1");
                    }
                }
                else
                {
                    Debug.Log("timer is frozen, you cannot activate the timer stop power up");
                }
            }

            // extra life
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
            {
                Debug.Log("Player clicked on 2");
                if (!resurrect)
                {
                    if (CurrencyUtils.getShopItemQuantity("2"))
                    {
                        Debug.Log("player uses item number 2");
                        Shop_Purchase.activatePowerUpUI("ExtraLife");
                        CurrencyUtils.displayQuantityDynamic("2", "Text_ExtraLife_Qty", "x: ");
                        resurrect = !resurrect;

                    }
                }
                else
                {
                    Shop_Purchase.deactivatePowerUpUI("ExtraLife");
                    resurrect = !resurrect;

                }

            }

            // word/score multiplier
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
            {
                Debug.Log("Player clicked on 3");
                // if (CurrencyUtils.useShopItem("3"))
                // activate score multiplier
                if(word.getMultiplier() != 2)
                {
                    if(CurrencyUtils.getShopItemQuantity("3"))
                    {
                        Debug.Log("player uses item number 3 - word/score multiplier");
                        // TwoX temp_twoX = new TwoX();
                        TwoX temp_twoX = ScriptableObject.CreateInstance<TwoX>();
                        temp_twoX.Activate();
                        Shop_Purchase.activatePowerUpUI("ScoreMultiplier");
                        CurrencyUtils.displayQuantityDynamic("3","Text_ScoreMultiplier_Qty","x: ");
                    }
                }
                // deactivate power up
                else
                {
                    Debug.Log("Score multiplier already activated score multiplier at this point: " + word.getMultiplier().ToString());
                    word.setMultiplier(1);
                    Shop_Purchase.deactivatePowerUpUI("ScoreMultiplier");
                    Debug.Log("Score multiplier post: " + word.getMultiplier().ToString());

                }

            }

            // anagram
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
            {
                Debug.Log("Player clicked on 4");
                if(!Anagram.isActivated())
                {
                    if (CurrencyUtils.getShopItemQuantity("4"))
                    {
                        Anagram.Activate();
                        Shop_Purchase.activatePowerUpUI("Anagram");
                        Debug.Log("player uses item number 4");
                        CurrencyUtils.displayQuantityDynamic("4","Text_Anagram_Qty","x: ");

                    }
                } else{
                    Anagram.reset();
                    Shop_Purchase.deactivatePowerUpUI("Anagram");
                }
            }

            /*
            // pause time
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
            {
                Debug.Log("Player clicked on 5");
                if (CurrencyUtils.useShopItem("5"))
                {
                    // timer.StopTimer()
                    StartCoroutine(StopTime(1.0f, 5.0f));
                    // timer.StartTimer();
                    Shop_Purchase.activatePowerUpUI("PauseTime");

                    Debug.Log("player uses item number 5");
                }
            }

            // prefix/suffix
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
            {
                Debug.Log("Player clicked on 6");
                if (CurrencyUtils.useShopItem("6"))
                {
                    Debug.Log("player uses item number 6");
                    // SuffixPU_score_version temp = new SuffixPU_score_version();
                    // temp.Activate_function();
                    SuffixPU_score_version.Activate_function();
                }
            }
            */

            // toggle half gravity for testing
            if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
            {
                if (GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale == 1.0f)
                {
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 0.7f;
                }
                else
                {
                    GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                }
            }
        }
    }

    // initialization of the item count in the left hand panel
    public void UpdateShopItemCount() {
        CurrencyUtils.displayQuantityDynamic("1","Text_PauseTime_Qty","x: ");
        CurrencyUtils.displayQuantityDynamic("2","Text_ExtraLife_Qty","x: ");
        CurrencyUtils.displayQuantityDynamic("3","Text_ScoreMultiplier_Qty","x: ");
        CurrencyUtils.displayQuantityDynamic("4","Text_Anagram_Qty","x: ");
        return;
    }

    // stop timer for 5 seconds
    public IEnumerator StopTime(float fadeTime, float totalTime)
    {
        // frost component
        FrostEffect frostEffect = GameObject.Find("Main Camera").GetComponent<FrostEffect>();

        if (timer.isTimerRunning()) {
            timer.StopTimer();
            timer.pauseTimeActivated();
        }
        Debug.Log("StopTime Activated, timer paused");

        float startAmount = frostEffect.FrostAmount;

        // fade frost in
        for (float f = 0.0f; f < fadeTime; f += Time.deltaTime)
        {
            frostEffect.FrostAmount = Mathf.Lerp(startAmount, 0.25f, f);
            yield return null;
        }

        yield return new WaitForSeconds(totalTime - fadeTime * 2.0f);

        Debug.Log("Time Returned Restarted Timer");
        if (!timer.isTimerRunning()) {
            timer.StartTimer();
            timer.pauseTimeDeactivated();
        }

        startAmount = frostEffect.FrostAmount;

        // fade frost out
        for (float f = 0.0f; f < fadeTime; f += Time.deltaTime)
        {
            frostEffect.FrostAmount = Mathf.Lerp(startAmount, 0.0f, f);
            yield return null;
        }

        Shop_Purchase.deactivatePowerUpUI("PauseTime");
    }

    private void InitiateBounce(string side)
    {
        if (side == "left")
        {
            StartCoroutine(BounceLeft());
        }
        else
        {
            currSprite.flipX = true;
            StartCoroutine(BounceRight());
        }
        bounceBackToCenter = true;
        bounceBackTargetPos = new Vector3(0, transform.position.y + 3f, 0);
        isBouncingBack = true;
        rb.gravityScale = 0;
        rb.velocity = Vector3.zero;

        wallBounceSound.Play();
        letterCollectSound.pitchRange = new Vector2(1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (started && rb.velocity.y < 0.0f)
        {
            StartCoroutine(AnimateJump());

            // Squish and Stretch Animation
            height.GetComponent<Animator>().SetTrigger("Bounce");
            Transform transform = collision.transform;
            if (transform.childCount > 0)
            {
                transform.GetChild(0).GetComponent<Animator>().SetTrigger("Bounce");
            }

            // Reset Gravity
            if (Platform.activated)
            {
                GameObject.Find("Player").GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                Platform.activated = false;
            }

            rb.velocity = new Vector2(rb.velocity.x, 10.0f);

            NewLetterPlatform letterPlatform = collision.GetComponent<NewLetterPlatform>();
            if (letterPlatform)
            {
                if (letterPlatform.collectible is LetterClass && ((LetterClass)letterPlatform.collectible).Letter != '_' && !letterPlatform.HasBeenCollected())
                {
                    letterCollectSound.Play(null, 0.2f);
                    letterCollectSound.pitchRange = new Vector2(letterCollectSound.pitchRange.x + 0.1f, letterCollectSound.pitchRange.y + 0.1f);
                }

                letterPlatform.Activate();
            }

            JumpPlatform jumpPlatform = collision.GetComponent<JumpPlatform>();
            if (jumpPlatform)
            {
                jumpPlatform.Activate();
            }

            bounceSound.Play();
        }
    }
    IEnumerator AnimateJump()
    {
        currSprite.sprite = playerGround;
        yield return new WaitForSeconds(0.3f);
        currSprite.sprite = playerJump;
    }
    IEnumerator BounceLeft()
    {
        currSprite.sprite = playerSubmit;
        yield return new WaitForSeconds(0.3f);
        currSprite.sprite = playerJump;
    }
    IEnumerator BounceRight()
    {
        currSprite.sprite = playerSubmit;
        yield return new WaitForSeconds(0.3f);
        currSprite.sprite = playerJump;
        currSprite.flipX = false;
    }
}