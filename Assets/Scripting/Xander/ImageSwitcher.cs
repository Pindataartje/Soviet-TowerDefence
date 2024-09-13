using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Image[] images;
    private int currentIndex = 0;

    void Start()
    {
        UpdateImageDisplay();
    }

    public void ShowNextImage()
    {
        images[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex + 1) % images.Length;
        UpdateImageDisplay();
    }

    public void ShowPreviousImage()
    {
        images[currentIndex].gameObject.SetActive(false);
        currentIndex = (currentIndex - 1 + images.Length) % images.Length;
        UpdateImageDisplay();
    }

    private void UpdateImageDisplay()
    {
        foreach (var image in images)
        {
            image.gameObject.SetActive(false);
        }
        images[currentIndex].gameObject.SetActive(true);
    }
}