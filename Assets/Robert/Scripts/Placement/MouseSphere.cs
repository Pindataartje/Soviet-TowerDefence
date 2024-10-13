using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseSphere : MonoBehaviour
{
    [Header("Ground Layer")]
    public LayerMask placementLayer;
    [Header("Runtime Only")]
    public bool cursorOnTower;
    public TowerOptions options;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            transform.position = hit.point;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Tower")
        {
            other.GetComponent<TowerOptions>();
            cursorOnTower = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tower")
        {
            cursorOnTower = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Tower")
        {
            cursorOnTower = false;
        }
    }
}
