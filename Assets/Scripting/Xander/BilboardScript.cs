using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BilboardScript : MonoBehaviour
{
    [SerializeField] private BillboardType billboardType;
    
    public enum BillboardType { LookAtCamera, CameraForward };

    private void LateUpdate()
    {
        switch (billboardType)
        {
            case BillboardType.LookAtCamera:
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
                break;
            case BillboardType.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            default:
                break;
        }
    }
}