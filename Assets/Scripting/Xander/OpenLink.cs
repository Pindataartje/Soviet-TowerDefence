using UnityEngine;
using UnityEngine.UI;

public class OpenLink : MonoBehaviour
{
    public Button yourButton; // Assign this in the Inspector
    public string url = "https://forms.gle/APMCBQLo2BGiaChJ6"; // The URL to open

    void Start()
    {
        // Ensure the button is assigned
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(OpenURL);
        }
        else
        {
            Debug.LogError("Button not assigned in the Inspector.");
        }
    }

    void OpenURL()
    {
        Application.OpenURL(url);
    }
}