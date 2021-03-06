using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // TODO: maybe game timers observers based off the Timer class
    private float timer;
    [Range(1, 60)][SerializeField] private int timeBetweenLevels = 45;

    [SerializeField] private PlatformGenerator randomPlatforms;
    [SerializeField] private StreamSpawning streamSpawning;

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
        int temp = Mathf.FloorToInt(timer / timeBetweenLevels);
        if (temp >= difficulties.Length) temp = difficulties.Length-1;

        currentDifficulty = difficulties[temp];

        if (previousDifficulty != currentDifficulty)
        {
            previousDifficulty = currentDifficulty;
            randomPlatforms.UpdateDifficulty(currentDifficulty);
            streamSpawning.UpdateDictionary(currentDifficulty.GetDictionary());
        }
    }
}
