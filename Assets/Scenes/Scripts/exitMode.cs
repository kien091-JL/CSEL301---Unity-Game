using UnityEngine;

public class exitMode : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Game Closed");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}