using UnityEngine;
using TMPro;

public class LapTime : MonoBehaviour
{
    public TextMeshProUGUI lapTimeText;
    public TextMeshProUGUI bestTimeText;
    public TextMeshProUGUI finishTimeText;
    public TextMeshProUGUI checkpointText;

    public GameObject finishPanel;

    public int totalCheckpoints = 5;

    private bool raceStarted = false;
    private bool raceFinished = false;
    private bool finishTriggered = false;

    private int currentCheckpoint = 0;

    private float currentLapTime = 0f;
    private float bestLapTime = Mathf.Infinity;

    void Start()
    {
        Time.timeScale = 1f;

        if (finishPanel != null)
            finishPanel.SetActive(false);

        bestLapTime = PlayerPrefs.GetFloat("BEST_TIME", Mathf.Infinity);
    }

    void Update()
    {
        if (!raceStarted || raceFinished)
            return;

        currentLapTime += Time.deltaTime;

        lapTimeText.text = "Lap: " + currentLapTime.ToString("F2");

        checkpointText.text =
            "Checkpoint " + currentCheckpoint + "/" + totalCheckpoints;

        // 🏆 UPDATE BEST TIME DISPLAY LIVE
        bestTimeText.text = bestLapTime == Mathf.Infinity
            ? "Best: 00:00:00"
            : "Best: " + bestLapTime.ToString("F2");
    }

    public void StartRace()
    {
        raceStarted = true;
        Debug.Log("RACE STARTED CALLED");
    }

    public void HitCheckpoint(int index)
    {
        if (!raceStarted || raceFinished) return;

        if (index == currentCheckpoint)
        {
            currentCheckpoint++;
            Debug.Log("Checkpoint counted: " + currentCheckpoint);
        }
    }

    public void HitFinish(Rigidbody carRb)
    {
        Debug.Log("FINISH TRIGGERED");

        if (!raceStarted || finishTriggered) return;

        finishTriggered = true;
        raceFinished = true;
        SaveLeaderboard();

        // 🏆 SAVE BEST TIME
        if (currentLapTime < bestLapTime)
        {
            bestLapTime = currentLapTime;

            PlayerPrefs.SetFloat("BEST_TIME", bestLapTime);
            PlayerPrefs.Save();
        }

        if (finishPanel != null)
            finishPanel.SetActive(true);

        if (finishTimeText != null)
            finishTimeText.text = "TIME: " + currentLapTime.ToString("F2");

        if (carRb != null)
        {
            carRb.linearVelocity = Vector3.zero;
            carRb.angularVelocity = Vector3.zero;
            carRb.isKinematic = true;
        }
        string playerName = PlayerPrefs.GetString("PLAYER_NAME", "Player");

        PlayerPrefs.SetString("LAST_NAME", playerName);
        PlayerPrefs.SetFloat("LAST_TIME", currentLapTime);
        PlayerPrefs.Save();

        void SaveLeaderboard()
        {
            string playerName =
                PlayerPrefs.GetString(
                    "PLAYER_NAME",
                    "Player");

            float time =
                currentLapTime;

            int count =
                PlayerPrefs.GetInt(
                    "LB_COUNT",
                    0);

            PlayerPrefs.SetString(
                "LB_NAME_" + count,
                playerName);

            PlayerPrefs.SetFloat(
                "LB_TIME_" + count,
                time);

            PlayerPrefs.SetInt(
                "LB_COUNT",
                count + 1);

            PlayerPrefs.Save();

            Debug.Log(
                "Leaderboard Entry Saved");
        }
       
        Time.timeScale = 0f;
    }
}