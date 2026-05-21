using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LeaderboardRowUI : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI timeText;

    public Image backgroundImage;

    [Header("Row Colors (optional)")]
    public Color normalColor = new Color(1f, 1f, 1f, 0.15f);
    public Color highlightColor = new Color(1f, 0.2f, 0.2f, 0.35f);

    public void SetRowData(int rank, string playerName, float time)
    {
        // Rank
        if (rankText != null)
            rankText.text = rank.ToString();

        // Name
        if (nameText != null)
            nameText.text = playerName;

        // Time formatting
        if (timeText != null)
        {
            TimeSpan ts = TimeSpan.FromSeconds(time);

            timeText.text = string.Format(
                "{0:D2}:{1:D2}.{2:D3}",
                ts.Minutes,
                ts.Seconds,
                ts.Milliseconds
            );
        }

        // Background FIX (this is what you're missing visually)
        if (backgroundImage != null)
        {
            backgroundImage.enabled = true;

            // Ensure sprite is visible
            if (backgroundImage.sprite == null)
            {
                Debug.LogWarning("Leaderboard row background has no sprite assigned!");
            }

            // Default look
            backgroundImage.color = normalColor;

            // Ensure it renders on top of nothing broken
            backgroundImage.raycastTarget = false;
        }
    }

    // Optional: call this when player is rank 1 or selected
    public void SetHighlight(bool isTop)
    {
        if (backgroundImage != null)
        {
            backgroundImage.color = isTop ? highlightColor : normalColor;
        }
    }
}