using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Required to manipulate UI Images and Colors

public class PauseMenu : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject pauseMenuUI;   // The main pause menu panel
    public GameObject optionsMenuUI;  // The options panel containing your sliders

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

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false); 
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void OpenOptions()
    {

        // Prints the requested message to the Unity Console
        Debug.Log("option panel open");

        pauseMenuUI.SetActive(false);  // Hide main pause menu
        optionsMenuUI.SetActive(true);  // Show options panel
    }

    public void CloseOptions()
    {

        optionsMenuUI.SetActive(false); // Hide options panel
        pauseMenuUI.SetActive(true);   // Bring back main pause menu
    }

    // Call this when clicking the "Quit" button
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        
        // Loads the scene by name instead of build index
        SceneManager.LoadScene("TitleScreen");
    }
}