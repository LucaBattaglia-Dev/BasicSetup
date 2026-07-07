using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UniversalUIManager : MonoBehaviour
{
    [Header("Scene Settings")]
    [Tooltip("Check this ONLY if the scene is allowed to be paused (e.g., gameplay scenes).")]
    [SerializeField] private bool canThisSceneBePaused = false;

    [Header("Audio Configurations")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string masterVolumeParameterName = "MasterVolume";

    private GameObject titleScreenPanel;
    private GameObject pauseMenuPanel;
    private GameObject optionsMenuPanel;
    private Slider masterVolumeSlider;
    private Slider brightnessSlider;
    private Image brightnessOverlay;

    private bool isPaused = false;

    private void Awake()
    {
        AutoAssignReferences();
    }

    private void Start()
    {
        InitializeUI();
        LoadSettings();
    }

    private void Update()
    {
        if (canThisSceneBePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    private void AutoAssignReferences()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        if (canvas == null) return;

        Transform canvasTransform = canvas.transform;

        Transform mainMenuTransform = canvasTransform.Find("MainMenuPanel");
        if (mainMenuTransform != null) titleScreenPanel = mainMenuTransform.gameObject;

        Transform pauseMenuTransform = canvasTransform.Find("PauseMenuPanel");
        if (pauseMenuTransform != null) pauseMenuPanel = pauseMenuTransform.gameObject;

        Transform optionsMenuTransform = canvasTransform.Find("OptionsMenuPanel");
        if (optionsMenuTransform != null) optionsMenuPanel = optionsMenuTransform.gameObject;

        Transform overlayTransform = canvasTransform.Find("BrightnessOverlay");
        if (overlayTransform != null) brightnessOverlay = overlayTransform.GetComponent<Image>();

        if (optionsMenuPanel != null)
        {
            Transform volumeSliderTransform = optionsMenuPanel.transform.Find("OptionsPanel/VolumePanel/Slider");
            if (volumeSliderTransform != null) masterVolumeSlider = volumeSliderTransform.GetComponent<Slider>();

            Transform brightnessSliderTransform = optionsMenuPanel.transform.Find("OptionsPanel/BrightnessPanel/Slider");
            if (brightnessSliderTransform != null) brightnessSlider = brightnessSliderTransform.GetComponent<Slider>();
        }
    }

    private void InitializeUI()
    {
        if (canThisSceneBePaused)
        {
            if (titleScreenPanel != null) titleScreenPanel.SetActive(false);
            if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
            if (optionsMenuPanel != null) optionsMenuPanel.SetActive(false);
            Time.timeScale = 1f; 
        }
        else
        {
            if (titleScreenPanel != null) titleScreenPanel.SetActive(true);
            if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
            if (optionsMenuPanel != null) optionsMenuPanel.SetActive(false);
        }
    }

    private void LoadSettings()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.onValueChanged.RemoveAllListeners();
            masterVolumeSlider.onValueChanged.AddListener(SetMasterVolume);
            float savedVol = PlayerPrefs.GetFloat("MasterVolume", 1f);
            masterVolumeSlider.value = savedVol;
            SetMasterVolume(savedVol);
        }

        if (brightnessSlider != null)
        {
            brightnessSlider.onValueChanged.RemoveAllListeners();
            brightnessSlider.onValueChanged.AddListener(SetBrightness);
            float savedBrightness = PlayerPrefs.GetFloat("Brightness", 0f);
            brightnessSlider.value = savedBrightness;
            SetBrightness(savedBrightness);
        }
    }

    #region Sliders & Audio

    public void SetMasterVolume(float value)
    {
        if (audioMixer == null) return;
        float dB = value > 0 ? Mathf.Log10(value) * 20f : -80f;
        audioMixer.SetFloat(masterVolumeParameterName, dB);
        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetBrightness(float value)
    {
        if (brightnessOverlay == null) return;
        Color color = brightnessOverlay.color;
        color.a = 1f - value; 
        brightnessOverlay.color = color;
        PlayerPrefs.SetFloat("Brightness", value);
    }

    #endregion

    #region Navigation / Button Controls

    // --- Options Sub-Menu Back Actions ---

    // Assign this to your BackButton OnClick in your Pause Screen Scene
    public void BackPauseButton()
    {
        if (optionsMenuPanel != null) optionsMenuPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }

    // Assign this to your BackButton OnClick in your Title Screen Scene
    public void BackTitleButton()
    {
        if (optionsMenuPanel != null) optionsMenuPanel.SetActive(false);
        if (titleScreenPanel != null) titleScreenPanel.SetActive(true);
    }

    // --- Standard Navigation ---

    public void StartGame(string gameplaySceneName)
    {
        SceneManager.LoadScene(gameplaySceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (!canThisSceneBePaused) return;
        isPaused = true;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void ResumeGame()
    {
        isPaused = false;
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (optionsMenuPanel != null) optionsMenuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OpenPauseOptions()
    {
        Debug.Log("option panel open");
        pauseMenuPanel.SetActive(false);  // Hide main pause menu
        optionsMenuPanel.SetActive(true);  // Show options panel
    }
    
    public void OpenTitleOptions()
    {
        Debug.Log("option panel open");
        titleScreenPanel.SetActive(false);  // Hide main title menu
        optionsMenuPanel.SetActive(true);  // Show options panel
    }


    public void GoToMainMenu(string mainMenuSceneName)
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(mainMenuSceneName);
    }

    #endregion
}