using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;

public class PlayerNameInput : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject nameInputPanel;

    public TMP_InputField nameInput;
    public TMP_Dropdown nameDropdown;

    private List<string> savedNames = new List<string>();
    public static string selectedName;

    void Start()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (nameInputPanel != null) nameInputPanel.SetActive(false);
    }

    public void ShowNamePanel()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (nameInputPanel != null) nameInputPanel.SetActive(true);
    }

    // 🔥 START BUTTON (CONFIRM + GO TO CAR SELECTION)
    public void StartGame()
    {
        if (nameInput == null || nameDropdown == null)
        {
            Debug.LogError("Missing TMP references!");
            return;
        }

        string name = nameInput.text;

        if (string.IsNullOrEmpty(name)) return;

        savedNames.Add(name);

        nameDropdown.options.Add(new TMP_Dropdown.OptionData(name));
        nameDropdown.RefreshShownValue();

        selectedName = name;

        Debug.Log("Loading Car Selection Scene...");

        SceneManager.LoadScene("Car_Selection"); // make sure this matches EXACT name
    }

    public void OnDropdownSelected(int index)
    {
        if (index < 0 || index >= savedNames.Count) return;

        selectedName = savedNames[index];
        nameInput.text = selectedName;
    }

    public void CancelToMainMenu()
    {
        if (nameInput != null) nameInput.text = "";

        if (nameInputPanel != null) nameInputPanel.SetActive(false);
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);

        SceneManager.LoadScene(0); // optional (only if you really want reload)
    }
}