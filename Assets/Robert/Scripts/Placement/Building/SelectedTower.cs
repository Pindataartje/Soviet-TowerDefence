using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTower : MonoBehaviour
{
    [Header("Tower Layer")]
    public LayerMask placementLayer;
    [Space]
    public float rotateSpeed;
    public bool objectIsOnPath;
    BuildMenu buildMenu;
    GameObject buildManager;

    private void Start()
    {
        buildManager = GameObject.FindGameObjectWithTag("BuildManager");
        buildMenu = buildManager.GetComponent<BuildMenu>();
    }
    public void Update()
    {
        if(!objectIsOnPath)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
            buildMenu.towerIsNotOnPath = true;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            buildMenu.towerIsNotOnPath = false;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, placementLayer))
        {
            transform.position = hit.point;
        }
        RotateTower();
    }
    public void RotateTower()
    {
        if(Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Rotating left");
            transform.Rotate(Vector3.down * rotateSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("Rotating right");
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}
