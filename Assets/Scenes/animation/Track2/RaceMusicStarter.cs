using UnityEngine;

public class RaceMusicStarter : MonoBehaviour
{
    private void Start()
    {
        // Switch to race music when Race scene starts
        MusicManager.Instance.PlayMusic("RACE BG MUSIC");
    }
}