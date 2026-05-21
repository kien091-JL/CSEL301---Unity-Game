using UnityEngine;
using System.Collections.Generic;

public class LeaderboardManager : MonoBehaviour
{
    [Header("UI References")]
    public Transform rowContainer;
    public GameObject rowPrefab;

    [Header("Panels")]
    public GameObject leaderboardPanel;

    private List<Entry> entries = new List<Entry>();

    void Start()
    {
        LoadLeaderboard();
        DisplayLeaderboard();

        // Show leaderboard when opened
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(true);
    }

    // LOAD SAVED DATA
    void LoadLeaderboard()
    {
        entries.Clear();

        int count = PlayerPrefs.GetInt("LB_COUNT", 0);

        for (int i = 0; i < count; i++)
        {
            Entry e = new Entry();

            e.playerName = PlayerPrefs.GetString("LB_NAME_" + i);
            e.time = PlayerPrefs.GetFloat("LB_TIME_" + i);

            entries.Add(e);
        }

        // Sort fastest first
        entries.Sort((a, b) => a.time.CompareTo(b.time));
    }

    // DISPLAY UI ROWS
    void DisplayLeaderboard()
    {
        foreach (Transform child in rowContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < entries.Count; i++)
        {
            GameObject row = Instantiate(rowPrefab, rowContainer);

            LeaderboardRowUI rowUI = row.GetComponent<LeaderboardRowUI>();

            rowUI.SetRowData(
                i + 1,
                entries[i].playerName,
                entries[i].time
            );
        }
    }

    // ❌ X BUTTON (just closes leaderboard)
    public void CloseLeaderboard()
    {
        if (leaderboardPanel != null)
            leaderboardPanel.SetActive(false);
    }

    class Entry
    {
        public string playerName;
        public float time;
    }
}