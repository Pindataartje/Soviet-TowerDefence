using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTower : MonoBehaviour
{
    Vector3 pos;

    float speed = 1.0f;
    public void Update()
    {
        pos = Input.mousePosition;
        pos.z = speed * Time.deltaTime;
        transform.position = Camera.main.ScreenToWorldPoint(pos);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            Color startColor = hit.transform.GetComponent<MeshRenderer>().material.color;

            if (hit.transform.tag == "Buildable")
            {
                GetComponent<MeshRenderer>().material.color = Color.green;
            }
            if (hit.transform.tag == "EnemyPath")
            {
                GetComponent<MeshRenderer>().material.color = Color.red;
            }
        }
    }
}
