using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cameraSpawn;
    bool goBackToSpawn;

    float timePassed;
    public float lerpTime;

    public float speed;

    float horizontal;
    float vertical;

    Rigidbody rb;
    
    Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection.x = horizontal;
        moveDirection.z = vertical;

        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);

        if(goBackToSpawn)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= lerpTime)
            {
                timePassed = lerpTime;
            }
            float lerpSpeed = timePassed / lerpTime;
            transform.position = Vector3.Lerp(transform.position, cameraSpawn.transform.position, lerpSpeed);
            
            if(transform.position == cameraSpawn.transform.position)
            {
                goBackToSpawn = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Border")
        {
            goBackToSpawn = true;
        }
    }
}
