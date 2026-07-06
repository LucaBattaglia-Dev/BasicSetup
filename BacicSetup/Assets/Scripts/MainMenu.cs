using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required to manipulate UI Images and Colors

public class MainMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuUI;    // The main title screen panel
    public GameObject optionsMenuUI;  // The options panel with your sliders

    [Header("Click Settings")]
    [Tooltip("How much darker the button gets when clicked. (0 = pitch black, 1 = normal color)")]
    [Range(0f, 1f)]
    public float darkMultiplier = 0.6f;

    void Start()
    {
        // Ensure the main menu is visible and options are hidden when the game starts
        if (mainMenuUI != null) mainMenuUI.SetActive(true);
        if (optionsMenuUI != null) optionsMenuUI.SetActive(false);
    }

    // Call this when clicking the "Start Game" or "Play" button
    public void StartGame(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);
        
        // Loads the scene at Build Index 1
        SceneManager.LoadScene(1);
    }

    // Call this when clicking the "Options" button
    public void OpenOptions(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);

        mainMenuUI.SetActive(false);  // Hide main title screen
        optionsMenuUI.SetActive(true);  // Show options panel
    }

    // Call this when clicking the "Back" button inside the Options Menu
    public void CloseOptions(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);

        optionsMenuUI.SetActive(false); // Hide options panel
        mainMenuUI.SetActive(true);   // Bring back main title screen
    }

    // Call this when clicking the "Quit" button
    public void QuitGame(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);

        Debug.Log("Game is shutting down..."); // Confirms it works inside the Unity Editor
        Application.Quit(); // Closes the actual built application
    }

    // Helper method that manually darkens the specific button that was clicked
    private void DarkenButtonColor(Button button)
    {
        if (button != null)
        {
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                // Multiplies the current RGB values to create a darker version of its base color
                Color darkerColor = buttonImage.color;
                darkerColor.r *= darkMultiplier;
                darkerColor.g *= darkMultiplier;
                darkerColor.b *= darkMultiplier;
                
                buttonImage.color = darkerColor;
            }
        }
    }
}