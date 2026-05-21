using UnityEngine;
using UnityEngine.SceneManagement;

public class Finished : MonoBehaviour
{
    public void MainMenu()
    {
        Time.timeScale = 1f;

        // Add these two lines to unlock and show your mouse cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(0);
    }

    public void Leaderboard()
    {
        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(6);
    }
}
