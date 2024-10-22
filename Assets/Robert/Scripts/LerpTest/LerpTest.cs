using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform target;
    [Space]
    public float lerpSpeed;
    public float valueToLerp;
    public float interpolateValue;
    public void Start()
    {
        valueToLerp = 1 / lerpSpeed;
    }
    void Update()
    {
        interpolateValue += Time.deltaTime / lerpSpeed;

        if(interpolateValue == 1)
        {
            Debug.Log(interpolateValue);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vector3.Lerp(transform.position, target.position, interpolateValue);
        }
    }
}
