using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ResolutionChanger : MonoBehaviour
{
    public GameObject image1024x576;
    public GameObject image1152x648;
    public GameObject image1280x720;
    public GameObject image1366x768;
    public GameObject image1600x900;
    public GameObject image1920x1080;
    public GameObject image2560x1440;
    public GameObject image3840x2160;

    public GameObject fullscreenImage;
    public GameObject windowedImage;

    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;

    private int currentResolutionIndex = 5;
    private int savedResolutionIndex;
    private float savedMusicVolume;
    private float savedSFXVolume;

    private Resolution[] resolutions = new Resolution[]
    {
        new Resolution { width = 1024, height = 576 },
        new Resolution { width = 1152, height = 648 },
        new Resolution { width = 1280, height = 720 },
        new Resolution { width = 1366, height = 768 },
        new Resolution { width = 1600, height = 900 },
        new Resolution { width = 1920, height = 1080 },
        new Resolution { width = 2560, height = 1440 },
        new Resolution { width = 3840, height = 2160 }
    };

    void Start()
    {
        LoadSettings();
        InitializeResolution();
        InitializeVolume();

        // Start in fullscreen mode or use saved settings
        UpdateFullscreenWindowedDisplay();

        // Add listeners to sliders to handle volume changes
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void LoadSettings()
    {
        // Load settings from PlayerPrefs or set default values
        savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        savedMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        savedSFXVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        // Apply loaded settings
        currentResolutionIndex = savedResolutionIndex;
        musicSlider.value = savedMusicVolume * 100f;
        sfxSlider.value = savedSFXVolume * 100f;
    }

    private void InitializeResolution()
    {
        UpdateResolution();
        UpdateFullscreenWindowedDisplay();
    }

    private void InitializeVolume()
    {
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void ApplySettings()
    {
        // Save current settings to PlayerPrefs
        PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value / 100f);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value / 100f);
        PlayerPrefs.Save();
    }

    public void ResetToSavedSettings()
    {
        // Reset sliders and resolution to the saved settings
        currentResolutionIndex = savedResolutionIndex;
        musicSlider.value = savedMusicVolume * 100f;
        sfxSlider.value = savedSFXVolume * 100f;

        UpdateResolution();
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }

    public void IncreaseResolution()
    {
        if (currentResolutionIndex < resolutions.Length - 1)
        {
            currentResolutionIndex++;
            UpdateResolution();
        }
    }

    public void DecreaseResolution()
    {
        if (currentResolutionIndex > 0)
        {
            currentResolutionIndex--;
            UpdateResolution();
        }
    }

    public void SetFullscreen()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        UpdateFullscreenWindowedDisplay();
    }

    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
        UpdateFullscreenWindowedDisplay();
    }

    private void UpdateResolution()
    {
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        // Update active images based on resolution index
        image1024x576.SetActive(currentResolutionIndex == 0);
        image1152x648.SetActive(currentResolutionIndex == 1);
        image1280x720.SetActive(currentResolutionIndex == 2);
        image1366x768.SetActive(currentResolutionIndex == 3);
        image1600x900.SetActive(currentResolutionIndex == 4);
        image1920x1080.SetActive(currentResolutionIndex == 5);
        image2560x1440.SetActive(currentResolutionIndex == 6);
        image3840x2160.SetActive(currentResolutionIndex == 7);
    }

    private void UpdateFullscreenWindowedDisplay()
    {
        bool isFullscreen = Screen.fullScreenMode == FullScreenMode.FullScreenWindow;
        fullscreenImage.SetActive(isFullscreen);
        windowedImage.SetActive(!isFullscreen);
    }

    public void SetMusicVolume(float volume)
    {
        float dBValue = Mathf.Log10(Mathf.Clamp(volume / 100f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("MusicVolume", dBValue);
    }

    public void SetSFXVolume(float volume)
    {
        float dBValue = Mathf.Log10(Mathf.Clamp(volume / 100f, 0.0001f, 1f)) * 20f;
        audioMixer.SetFloat("SFXVolume", dBValue);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}