using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelObjectiveManager : MonoBehaviour
{
    [Header("Time Settings")]
    public float timeLimit = 60f;
    private float currentTime;
    private bool isGameOver = false;

    [Header("UI References")]
    public TextMeshProUGUI timerText;

    [Header("Penalty Settings")]
    public float deathPenalty = 5f;

    private SaturationManager satManager;

    void Start()
    {
        currentTime = timeLimit;
        // Updated to Unity 6 standards
        satManager = Object.FindFirstObjectByType<SaturationManager>();
    }

    void Update()
    {
        if (isGameOver) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            TimerRanOut();
        }
    }

    void UpdateTimerUI()
    {
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (currentTime <= 10f) timerText.color = Color.red;
    }

    public void ApplyDeathPenalty()
    {
        currentTime -= deathPenalty;
        StopAllCoroutines();
        StartCoroutine(FlashTimerRed());

        Debug.Log($"<color=orange>Penalty Applied!</color> Lost {deathPenalty}s.");

        if (currentTime <= 0)
        {
            currentTime = 0;
            TimerRanOut();
        }
    }

    private System.Collections.IEnumerator FlashTimerRed()
    {
        timerText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        timerText.color = Color.white;
    }

    void TimerRanOut()
    {
        isGameOver = true;
        Debug.Log("<color=red>TIME UP!</color> Restarting...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinLevel()
    {
        isGameOver = true;
        timerText.color = Color.green;
    }
}