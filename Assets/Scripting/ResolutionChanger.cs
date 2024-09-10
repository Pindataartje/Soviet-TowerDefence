using UnityEngine;

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

    private int currentResolutionIndex = 5;

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
        UpdateResolution();
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
    }

    public void SetWindowed()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    private void UpdateResolution()
    {
        Resolution resolution = resolutions[currentResolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        image1024x576.SetActive(currentResolutionIndex == 0);
        image1152x648.SetActive(currentResolutionIndex == 1);
        image1280x720.SetActive(currentResolutionIndex == 2);
        image1366x768.SetActive(currentResolutionIndex == 3);
        image1600x900.SetActive(currentResolutionIndex == 4);
        image1920x1080.SetActive(currentResolutionIndex == 5);
        image2560x1440.SetActive(currentResolutionIndex == 6);
        image3840x2160.SetActive(currentResolutionIndex == 7);
    }
}