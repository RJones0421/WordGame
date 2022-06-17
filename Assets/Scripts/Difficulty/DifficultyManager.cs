using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // TODO: maybe game timers observers based off the Timer class
    private float timer;

    [SerializeField] private PlatformGenerator randomPlatforms;
    [SerializeField] private PlatformGenerator commonStream;
    [SerializeField] private PlatformGenerator fullStream;

    [SerializeField] private Difficulty[] difficulties;
    private Difficulty currentDifficulty;
    private Difficulty previousDifficulty;

    private void Awake()
    {
        currentDifficulty = difficulties[0];
        previousDifficulty = difficulties[0];
        randomPlatforms.UpdateDifficulty(currentDifficulty);
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UpdateDifficulty();
    }

    private void UpdateDifficulty()
    {
        currentDifficulty = timer switch
        {
            < 30f => difficulties[0],
            < 60f => difficulties[1],
            < 110f => difficulties[2],
            _ => difficulties[3]
        };

        if (previousDifficulty != currentDifficulty)
        {
            print(currentDifficulty);
            previousDifficulty = currentDifficulty;
            randomPlatforms.UpdateDifficulty(currentDifficulty);
        }
    }
}
