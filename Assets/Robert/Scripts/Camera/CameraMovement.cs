using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cameraSpawn;
    bool goBackToSpawn;

    public float speed;
    public float lerpSpeed;
    public float elapsedTime;
    public float valueToLerp;

    public float distanceGroundToCamera;

    float horizontal;
    float vertical;

    Rigidbody rb;
    
    Vector3 moveDirection;

    private void Start()
    {
        valueToLerp = 1 / lerpSpeed;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        moveDirection.x = horizontal;
        moveDirection.z = vertical;

        rb.MovePosition(transform.position + moveDirection * speed * Time.deltaTime);
        if(horizontal == 0 && vertical == 0)
        {
            transform.position = transform.position;
        }

        if(goBackToSpawn)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpSpeed);
            transform.position = Vector3.Lerp(transform.position, cameraSpawn.transform.position, t);

            if (t >= 1f)
            {
                goBackToSpawn = false;
            }
        }
        StayAtSameHeight();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Border")
        {
            valueToLerp = 1 / lerpSpeed;
            goBackToSpawn = true;
        }
    }
    public void StayAtSameHeight()
    {
        //RaycastHit hit;
        //if(Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        //{
        //    if (hit.transform.tag == "Ground")
        //    {
        //        float soupGetal = distanceGroundToCamera - hit.distance;
        //        if (hit.distance < distanceGroundToCamera)
        //        {
        //            transform.position += new Vector3(0, soupGetal, 0);
        //        }

        //        float negatifSoupGetal = hit.distance - distanceGroundToCamera;
        //        if(hit.distance > distanceGroundToCamera)
        //        {
        //            transform.position -= new Vector3(0, negatifSoupGetal, 0);
        //        }
        //    }
        //}
    }
}
