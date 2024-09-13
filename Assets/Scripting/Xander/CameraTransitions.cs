using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraTransitions : MonoBehaviour
{
    public CinemachineVirtualCamera currentCamera;

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        currentCamera.Priority--;
        currentCamera = target;
        currentCamera.Priority++;
    }
}