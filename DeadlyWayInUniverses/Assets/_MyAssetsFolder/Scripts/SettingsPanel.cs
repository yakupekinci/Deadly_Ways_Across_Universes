using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class SettingsPanel : MonoBehaviour
{
    // Graphics Settings
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public TMP_Dropdown qualityDropdown;
    public Toggle vSyncToggle;
    public TMP_Dropdown antiAliasingDropdown;
    public TMP_Dropdown textureQualityDropdown;
    public TMP_Dropdown shadowQualityDropdown;

    // Audio Settings
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider voiceVolumeSlider;

    // Control Settings
    public Slider mouseSensitivitySlider;
    public Toggle invertMouseToggle;
    public Button keyBindingsButton;

    // Gameplay Settings
    public TMP_Dropdown difficultyDropdown;
    public Toggle subtitlesToggle;
    public Toggle tutorialsToggle;
    public TMP_Dropdown languageDropdown;

    // Accessibility Settings
    public Toggle colorBlindModeToggle;
    public TMP_Dropdown textSizeDropdown;
    public Toggle highContrastModeToggle;

    void Start()
    {
        // Initialize UI elements with current settings values
        InitializeSettings();
    }

    void InitializeSettings()
    {
        // Example: Set resolution options
        resolutionDropdown.options = GetResolutionOptions();
        resolutionDropdown.value = GetCurrentResolutionIndex();
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);

        // Example: Set full screen toggle
        fullScreenToggle.isOn = Screen.fullScreen;
        fullScreenToggle.onValueChanged.AddListener(OnFullScreenToggle);

        // Example: Set quality options
        qualityDropdown.options = GetQualityOptions();
        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.onValueChanged.AddListener(OnQualityChange);

        // Example: Set V-Sync toggle
        vSyncToggle.isOn = QualitySettings.vSyncCount > 0;
        vSyncToggle.onValueChanged.AddListener(OnVSyncToggle);

        // Example: Set anti-aliasing options
        antiAliasingDropdown.options = GetAntiAliasingOptions();
        antiAliasingDropdown.value = GetCurrentAntiAliasingIndex();
        antiAliasingDropdown.onValueChanged.AddListener(OnAntiAliasingChange);

        // Example: Set texture quality options
        textureQualityDropdown.options = GetTextureQualityOptions();
        textureQualityDropdown.value = QualitySettings.masterTextureLimit;
        textureQualityDropdown.onValueChanged.AddListener(OnTextureQualityChange);

        // Example: Set shadow quality options
        shadowQualityDropdown.options = GetShadowQualityOptions();
        shadowQualityDropdown.value = GetCurrentShadowQualityIndex();
        shadowQualityDropdown.onValueChanged.AddListener(OnShadowQualityChange);

        // Initialize Audio Settings
        masterVolumeSlider.value = AudioListener.volume;
        masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChange);

        // Initialize other audio sliders similarly...

        // Initialize Control Settings
        mouseSensitivitySlider.value = GetMouseSensitivity();
        mouseSensitivitySlider.onValueChanged.AddListener(OnMouseSensitivityChange);

        invertMouseToggle.isOn = IsMouseInverted();
        invertMouseToggle.onValueChanged.AddListener(OnInvertMouseToggle);

        // Initialize Gameplay Settings
        difficultyDropdown.options = GetDifficultyOptions();
        difficultyDropdown.value = GetCurrentDifficultyIndex();
        difficultyDropdown.onValueChanged.AddListener(OnDifficultyChange);

        subtitlesToggle.isOn = AreSubtitlesEnabled();
        subtitlesToggle.onValueChanged.AddListener(OnSubtitlesToggle);

        tutorialsToggle.isOn = AreTutorialsEnabled();
        tutorialsToggle.onValueChanged.AddListener(OnTutorialsToggle);

        languageDropdown.options = GetLanguageOptions();
        languageDropdown.value = GetCurrentLanguageIndex();
        languageDropdown.onValueChanged.AddListener(OnLanguageChange);

        // Initialize Accessibility Settings
        colorBlindModeToggle.isOn = IsColorBlindModeEnabled();
        colorBlindModeToggle.onValueChanged.AddListener(OnColorBlindModeToggle);

        textSizeDropdown.options = GetTextSizeOptions();
        textSizeDropdown.value = GetCurrentTextSizeIndex();
        textSizeDropdown.onValueChanged.AddListener(OnTextSizeChange);

        highContrastModeToggle.isOn = IsHighContrastModeEnabled();
        highContrastModeToggle.onValueChanged.AddListener(OnHighContrastModeToggle);
    }

    public List<TMP_Dropdown.OptionData> GetResolutionOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        // Your code to fill options...
        options.Add(new TMP_Dropdown.OptionData("1920x1080"));
        return options;
    }

    public int GetCurrentResolutionIndex()
    {
        // Your code to get current resolution index...
        return 0; // Return default or calculated index
    }

    public List<TMP_Dropdown.OptionData> GetQualityOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("High"));
        return options;
    }

    public int GetCurrentQualityIndex()
    {
        return QualitySettings.GetQualityLevel();
    }

    public List<TMP_Dropdown.OptionData> GetAntiAliasingOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("2x"));
        return options;
    }

    public int GetCurrentAntiAliasingIndex()
    {
        // Your code to get current anti-aliasing index...
        return 0; // Return default or calculated index
    }

    public List<TMP_Dropdown.OptionData> GetTextureQualityOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("High"));
        return options;
    }

    public List<TMP_Dropdown.OptionData> GetShadowQualityOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("High"));
        return options;
    }

    public int GetCurrentShadowQualityIndex()
    {
        // Your code to get current shadow quality index...
        return 0; // Return default or calculated index
    }

    public List<TMP_Dropdown.OptionData> GetDifficultyOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("Easy"));
        return options;
    }

    public int GetCurrentDifficultyIndex()
    {
        // Your code to get current difficulty index...
        return 0; // Return default or calculated index
    }

    public List<TMP_Dropdown.OptionData> GetLanguageOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("English"));
        return options;
    }

    public int GetCurrentLanguageIndex()
    {
        // Your code to get current language index...
        return 0; // Return default or calculated index
    }

    public List<TMP_Dropdown.OptionData> GetTextSizeOptions()
    {
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
        options.Add(new TMP_Dropdown.OptionData("Medium"));
        return options;
    }
 
    public int GetCurrentTextSizeIndex()
    {
        // Your code to get current text size index...
        return 0; // Return default or calculated index
    }
    // Get current mouse sensitivity value
    float GetMouseSensitivity()
    {
        // Replace with the actual implementation to get mouse sensitivity
        return PlayerPrefs.GetFloat("MouseSensitivity", 1.0f);
    }

    // Check if the mouse is inverted
    bool IsMouseInverted()
    {
        // Replace with the actual implementation to check if the mouse is inverted
        return PlayerPrefs.GetInt("InvertMouse", 0) == 1;
    }

    // Check if subtitles are enabled
    bool AreSubtitlesEnabled()
    {
        // Replace with the actual implementation to check if subtitles are enabled
        return PlayerPrefs.GetInt("SubtitlesEnabled", 1) == 1;
    }

    // Check if tutorials are enabled
    bool AreTutorialsEnabled()
    {
        // Replace with the actual implementation to check if tutorials are enabled
        return PlayerPrefs.GetInt("TutorialsEnabled", 1) == 1;
    }

    // Check if color blind mode is enabled
    bool IsColorBlindModeEnabled()
    {
        // Replace with the actual implementation to check if color blind mode is enabled
        return PlayerPrefs.GetInt("ColorBlindModeEnabled", 0) == 1;
    }

    // Check if high contrast mode is enabled
    bool IsHighContrastModeEnabled()
    {
        // Replace with the actual implementation to check if high contrast mode is enabled
        return PlayerPrefs.GetInt("HighContrastModeEnabled", 0) == 1;
    }


    // Example implementations of missing methods
    void OnResolutionChange(int index) { /* Your code here */ }
    void OnFullScreenToggle(bool isFullScreen) { /* Your code here */ }
    void OnQualityChange(int index) { /* Your code here */ }
    void OnVSyncToggle(bool isVSyncOn) { /* Your code here */ }
    void OnAntiAliasingChange(int index) { /* Your code here */ }
    void OnTextureQualityChange(int index) { /* Your code here */ }
    void OnShadowQualityChange(int index) { /* Your code here */ }
    void OnMasterVolumeChange(float volume) { /* Your code here */ }
    void OnMouseSensitivityChange(float sensitivity) { /* Your code here */ }
    void OnInvertMouseToggle(bool isInverted) { /* Your code here */ }
    void OnDifficultyChange(int index) { /* Your code here */ }
    void OnSubtitlesToggle(bool areSubtitlesEnabled) { /* Your code here */ }
    void OnTutorialsToggle(bool areTutorialsEnabled) { /* Your code here */ }
    void OnLanguageChange(int index) { /* Your code here */ }
    void OnColorBlindModeToggle(bool isColorBlindModeEnabled) { /* Your code here */ }
    void OnTextSizeChange(int index) { /* Your code here */ }
    void OnHighContrastModeToggle(bool isHighContrastModeEnabled) { /* Your code here */ }
}
