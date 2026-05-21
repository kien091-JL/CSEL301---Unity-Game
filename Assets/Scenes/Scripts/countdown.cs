using UnityEngine;
using TMPro;
using System.Collections;

public class countdown : MonoBehaviour
{
    public GameObject countdownPanel;
    public TextMeshProUGUI countdownText;
    public LapTime lapTime;

    private car1 playerCar;

    void Start()
    {
        StartCoroutine(WaitForCarAndCountdown());
    }

    IEnumerator WaitForCarAndCountdown()
    {
        // Wait until spawned player car exists
        while (playerCar == null)
        {
            playerCar = FindFirstObjectByType<car1>();
            yield return null;
        }

        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        // Disable movement during countdown
        playerCar.enabled = false;

        countdownPanel.SetActive(true);

        yield return Show("3");
        yield return Show("2");
        yield return Show("1");

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownPanel.SetActive(false);

        // Start lap timer
        if (lapTime == null)
        {
            lapTime = FindFirstObjectByType<LapTime>();
        }

        if (lapTime != null)
        {
            lapTime.StartRace();
        }

        // Enable car control
        playerCar.enabled = true;
    }

    IEnumerator Show(string num)
    {
        countdownText.text = num;
        yield return new WaitForSeconds(1f);
    }
}