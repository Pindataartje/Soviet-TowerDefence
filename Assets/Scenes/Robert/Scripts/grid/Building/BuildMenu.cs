using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    public Button[] towerButtons;

    public GameObject[] towerVignettePrefabs;
    public GameObject selectedVignette;

    public GameObject[] towers;

    public GameObject[] towersInMap;

    public int selectedTowerNumber;

    public GameObject buildMenuUI;

    public bool buildMenuIsOpen = false;
    public bool towerBeingPlaced = false;
    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (!buildMenuIsOpen)
            {
                buildMenuUI.SetActive(true);
                buildMenuIsOpen = true;
            }
            else
            {
                buildMenuUI.SetActive(false);
                buildMenuIsOpen = false;
            }
        }
        Selecting();
    }
    public void TowerOne()
    {
        selectedVignette = Instantiate(towerVignettePrefabs[0]);

        selectedTowerNumber = 0;
        towerBeingPlaced = true;
    }
    public void TowerTwo()
    {
        selectedVignette = Instantiate(towerVignettePrefabs[1]);

        selectedTowerNumber = 1;
        towerBeingPlaced = true;
    }
    public void TowerThree()
    {
        selectedVignette = Instantiate(towerVignettePrefabs[2]);

        selectedTowerNumber = 2;
        towerBeingPlaced = true;
    }
    public void TowerFour()
    {
        selectedVignette = Instantiate(towerVignettePrefabs[3]);

        selectedTowerNumber = 3;
        towerBeingPlaced = true;
    }

    private void Selecting()
    {
        if (towerBeingPlaced)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "Buildable")
                {
                    Debug.Log("You Can Place");
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Debug.Log(selectedVignette.transform.position);
                        Instantiate(towers[selectedTowerNumber], selectedVignette.transform.position, selectedVignette.transform.rotation);

                        towerBeingPlaced = false;

                        buildMenuUI.SetActive(false);
                        buildMenuIsOpen = false;

                        Destroy(selectedVignette);

                        Debug.Log("Tower is placed");
                    }
                }
                if (hit.transform.tag == "EnemyPath")
                {
                    Debug.Log("Its not really smart to place here");
                }
            }
        }
    }
}
