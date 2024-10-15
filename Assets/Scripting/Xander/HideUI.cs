using UnityEngine;

public class HideUI : MonoBehaviour
{
    public GameObject[] uiElements; // Array to hold UI GameObjects
    private bool isHidden = false;  // To track whether UI is hidden or shown

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleUI();
        }
    }

    // Function to toggle UI visibility
    private void ToggleUI()
    {
        isHidden = !isHidden;

        foreach (GameObject uiElement in uiElements)
        {
            if (uiElement != null)
            {
                uiElement.SetActive(!isHidden); // Show or hide the UI elements
            }
        }
    }
}