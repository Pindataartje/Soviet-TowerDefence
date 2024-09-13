using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTower : MonoBehaviour
{
    public void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            transform.position = hit.point;

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
