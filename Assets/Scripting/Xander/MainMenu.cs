using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen; // The loading screen image
    public Slider loadingSlider;     // The slider to show loading progress

    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneWithDelay(sceneIndex)); // Start loading with a slight delay
    }

    private IEnumerator LoadSceneWithDelay(int sceneIndex)
    {
        loadingScreen.SetActive(true);  // Activate the loading screen

        yield return new WaitForSeconds(0.1f); // Small delay to avoid glitch

        // Start loading the scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;  // Prevent the scene from activating immediately

        // Continue to update the slider based on the load progress
        while (!asyncLoad.isDone)
        {
            // asyncLoad.progress ranges from 0 to 0.9, so we normalize it to 0 to 1 for the slider
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progress;  // Update the slider

            // Check if the loading is complete (asyncLoad.progress reaches 0.9)
            if (asyncLoad.progress >= 0.9f)
            {
                loadingSlider.value = 1f;  // Ensure the slider is full

                yield return new WaitForSeconds(0.5f); // Wait a bit before activating the scene
                asyncLoad.allowSceneActivation = true;  // Activate the scene
            }

            yield return null;  // Wait for the next frame
        }
    }
}