using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required to manipulate UI Images and Colors

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuUI;    // The main title screen panel
    public GameObject optionsMenuUI;  // The options panel with your sliders


    void Start()
    {
        // Ensure the main menu is visible and options are hidden when the game starts
        if (mainMenuUI != null) mainMenuUI.SetActive(true);
        if (optionsMenuUI != null) optionsMenuUI.SetActive(false);
    }

    // Call this when clicking the "Start Game" or "Play" button
    public void StartGame()
    {
        // Loads the scene at Build Index 1
        SceneManager.LoadScene(1);
    }

    // Call this when clicking the "Options" button
    public void OpenOptions()
    {
        mainMenuUI.SetActive(false);  // Hide main title screen
        optionsMenuUI.SetActive(true);  // Show options panel
    }

    // Call this when clicking the "Back" button inside the Options Menu
    public void CloseOptions()
    {
        optionsMenuUI.SetActive(false); // Hide options panel
        mainMenuUI.SetActive(true);   // Bring back main title screen
    }

    // Call this when clicking the "Quit" button
    public void QuitGame()
    {
        Debug.Log("Game is shutting down..."); // Confirms it works inside the Unity Editor
        Application.Quit(); // Closes the actual built application
    }
}