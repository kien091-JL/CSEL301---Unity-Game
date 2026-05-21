using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMusicSwitcher : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Main Menu":
                MusicManager.Instance.PlayMusic("MAIN MENU BG MUSIC", 0.5f, true);
                break;
            case "Track1":
                MusicManager.Instance.PlayMusic("TRACK1 BG MUSIC");
                break;
            case "Track2":
                MusicManager.Instance.PlayMusic("TRACK2 BG MUSIC");
                break;
            case "Track3":
                MusicManager.Instance.PlayMusic("TRACK3 BG MUSIC");
                break;
        }
    }
}