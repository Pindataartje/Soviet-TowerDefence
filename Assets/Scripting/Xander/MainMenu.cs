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
        Time.timeScale = 1f; // Reset the time scale to normal in case the game is paused
        StartCoroutine(LoadSceneWithFakeProgress(sceneIndex)); // Start loading the scene with fake progress
    }

    public void LoadSceneNormally(int sceneIndex)
    {
        Time.timeScale = 1f; // Reset the time scale to normal in case the game is paused
        StartCoroutine(LoadSceneWithRealProgress(sceneIndex)); // Start loading the scene with real progress
    }

    // Fake loading with random progress and smooth movements
    private IEnumerator LoadSceneWithFakeProgress(int sceneIndex)
    {
        loadingScreen.SetActive(true);  // Activate the loading screen

        yield return new WaitForSeconds(0.1f); // Small delay to avoid glitches

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;  // Prevent immediate activation

        float fakeProgress = 0f;
        float targetProgress = 0f; // This will be used for smooth movement to the new target
        float maxDuration = Random.Range(2.5f, 3f); // Maximum loading time between 2.5 and 3 seconds
        float timer = 0f;

        while (!asyncLoad.isDone)
        {
            timer += Time.deltaTime;

            // Gradually increase the fake progress
            targetProgress += Random.Range(0.01f, 0.02f) * Time.deltaTime;
            targetProgress = Mathf.Clamp01(targetProgress);

            // Smoothly move the actual progress towards the target progress
            fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, Time.deltaTime * 0.5f); // Swift but smooth transition
            fakeProgress = Mathf.Clamp01(fakeProgress);

            // Simulate less frequent but larger "stuck" moments
            if (Random.Range(0f, 1f) < 0.05f) // 5% chance for a "stuck" moment
            {
                yield return new WaitForSeconds(Random.Range(0.3f, 0.5f)); // Simulate a pause

                // After the stuck moment, set a new target with a bigger jump
                targetProgress += Random.Range(0.1f, 0.2f); // Big value jump after getting stuck
                targetProgress = Mathf.Clamp01(targetProgress); // Ensure it doesn't go above 1
            }

            // Update the slider with the current fake progress
            loadingSlider.value = fakeProgress;

            // Ensure it doesn't take longer than maxDuration to load
            if (timer >= maxDuration || (fakeProgress >= 1f && asyncLoad.progress >= 0.9f))
            {
                loadingSlider.value = 1f; // Ensure slider reaches full
                asyncLoad.allowSceneActivation = true; // Activate the scene
            }

            yield return null;
        }
    }

    // Normal loading without fake progress
    private IEnumerator LoadSceneWithRealProgress(int sceneIndex)
    {
        loadingScreen.SetActive(true);  // Activate the loading screen

        yield return new WaitForSeconds(0.1f); // Small delay to avoid glitches

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            // Update the slider with the real progress (asyncLoad.progress goes from 0 to 0.9)
            loadingSlider.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);

            // Ensure slider reaches 1 when fully loaded
            if (asyncLoad.progress >= 0.9f)
            {
                loadingSlider.value = 1f;
            }

            yield return null;
        }
    }
}