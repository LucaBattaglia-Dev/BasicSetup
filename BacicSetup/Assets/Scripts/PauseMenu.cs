using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required to manipulate UI Images and Colors

public class PauseMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject pauseMenuUI;   // The main pause menu panel
    public GameObject optionsMenuUI;  // The options panel containing your sliders

    [Header("Click Settings")]
    [Tooltip("How much darker the button gets when clicked. (0 = pitch black, 1 = normal color)")]
    [Range(0f, 1f)]
    public float darkMultiplier = 0.6f;

    private static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Handled via Escape key (no button passed)
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false); 
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Resume(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);
        Resume();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false); 
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenOptions(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);

        // Prints the requested message to the Unity Console
        Debug.Log("option panel open");

        pauseMenuUI.SetActive(false);  // Hide main pause menu
        optionsMenuUI.SetActive(true);  // Show options panel
    }

    public void CloseOptions(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);

        optionsMenuUI.SetActive(false); // Hide options panel
        pauseMenuUI.SetActive(true);   // Bring back main pause menu
    }

    // Call this when clicking the "Quit" button
    public void QuitToMenu(Button clickedButton)
    {
        DarkenButtonColor(clickedButton);

        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(1);
    }

    // Helper method that manually darkens the specific button that was clicked
    private void DarkenButtonColor(Button button)
    {
        if (button != null)
        {
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                Color darkerColor = buttonImage.color;
                darkerColor.r *= darkMultiplier;
                darkerColor.g *= darkMultiplier;
                darkerColor.b *= darkMultiplier;
                
                buttonImage.color = darkerColor;
            }
        }
    }
}