using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardTest : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        transform.LookAt(target);
    }
}
