using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;
    public CinemachineVirtualCamera targetCamera;
    public GameObject objectToActivate;
    public float delay;

    public void SwitchCameraWithSettings()
    {
        SwitchCamera(targetCamera, objectToActivate, delay);
    }

    private void SwitchCamera(CinemachineVirtualCamera target, GameObject objectToActivate, float delay)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;

        StartCoroutine(ActivateObjectAfterDelay(objectToActivate, delay));
    }

    private IEnumerator ActivateObjectAfterDelay(GameObject objectToActivate, float delay)
    {
        yield return new WaitForSeconds(delay);

        objectToActivate.SetActive(true);
    }
}