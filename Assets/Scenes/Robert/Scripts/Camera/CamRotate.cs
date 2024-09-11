using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public Transform startTarget;
    public float lookAtSensX;
    public float lookAtSensY;

    public float mousePositionX;
    public float mousePositionY;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var mousePositionX = new Vector3(ray.origin.x, transform.position.y, startTarget.position.z);
        var mousePositionY = new Vector3(transform.position.x, ray.origin.y, startTarget.position.z);

        transform.LookAt(mousePositionX);
        transform.LookAt(mousePositionY);
    }
}
