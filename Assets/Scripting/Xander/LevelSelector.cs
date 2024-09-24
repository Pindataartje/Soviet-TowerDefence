using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;  // Add this for HDRP

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] cutsceneCameras; // Array of Cinemachine Virtual Cameras for the cutscene
    [SerializeField] private float transitionTime = 2f; // Time duration between each camera transition
    [SerializeField] private Volume globalVolume; // Global Volume to control post-processing effects
    [SerializeField] private float volumeTransitionTime = 2f; // Time for the global volume effect to intensify (e.g., fade or bloom)

    private VolumeProfile profile;

    private void Start()
    {
        if (globalVolume != null)
        {
            profile = globalVolume.profile;
        }

        // Set all cameras to their initial priority (low value) at the start
        foreach (var cam in cutsceneCameras)
        {
            cam.Priority = 10;
        }
    }

    public void OnButtonPress()
    {
        StartCoroutine(PlayCutsceneAndLoadScene());
    }

    private IEnumerator PlayCutsceneAndLoadScene()
    {
        foreach (var cam in cutsceneCameras)
        {
            SetCameraPriority(cam);
            yield return new WaitForSeconds(transitionTime); // Wait before transitioning to the next camera
        }

        yield return StartCoroutine(IntensifyGlobalVolume());

        LoadScene1Async();
    }

    private void SetCameraPriority(CinemachineVirtualCamera activeCamera)
    {
        foreach (var cam in cutsceneCameras)
        {
            // Set the active camera's priority higher than others
            cam.Priority = cam == activeCamera ? 11 : 10;
        }
    }

    private IEnumerator IntensifyGlobalVolume()
    {
        float currentTime = 0f;

        if (profile.TryGet(out Bloom bloom))  // Ensure you're using HDRP and Bloom is enabled in the Volume
        {
            float initialIntensity = bloom.intensity.value;
            float targetIntensity = 1f;

            while (currentTime < volumeTransitionTime)
            {
                currentTime += Time.deltaTime;
                float progress = currentTime / volumeTransitionTime;

                bloom.intensity.value = Mathf.Lerp(initialIntensity, targetIntensity, progress);

                yield return null;
            }

            bloom.intensity.value = targetIntensity;
        }
    }

    public async void LoadScene1Async()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(1);

        while (!asyncLoad.isDone)
        {
            await Task.Yield();
        }
    }
}