using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    [Header("Tower Vignettes")]
    public GameObject[] towerVignettePrefabs;
    public GameObject selectedVignette;

    [Header("Tower Prefabs")]
    public GameObject[] towers;

    [Header("Towers Placed (Only in runtime)")]
    public List<GameObject> towersInMap = new List<GameObject>();
    public bool towerIsNotOnPath = true;

    GameObject onSelectedTowerCanvas;
    int selectedTowerNumber;

    [Header("UI")]
    public GameObject buildMenuUI;

    bool buildMenuIsOpen = false;
    bool towerBeingPlaced = false;

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

        ATowerMenuIsOpen();

        Selecting();
    }
    #region TowerButtons
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
    #endregion
    #region Selecting
    private void Selecting()
    {
        
        if (towerBeingPlaced)
        {
            if(towerIsNotOnPath)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject placedTower = Instantiate(towers[selectedTowerNumber], selectedVignette.transform.position, selectedVignette.transform.rotation);
                    towersInMap.Add(placedTower);

                    towerBeingPlaced = false;

                    buildMenuUI.SetActive(false);
                    buildMenuIsOpen = false;

                    Destroy(selectedVignette);

                    Debug.Log("Tower is placed");
                }
            }
            else
            {
                Debug.Log("Its not really smart to place here");
            }
        }
    }
    #endregion
    public void ATowerMenuIsOpen()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.tag == "Tower")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    foreach (GameObject tower in towersInMap)
                    {
                        tower.GetComponent<TowerOptions>().TowerDeselect();
                        if (!tower.GetComponent<TowerOptions>().towerOptionsIsOpen)
                        {
                            hit.transform.GetComponent<TowerOptions>().TowerSelect();
                        }
                    }
                }
            }
        }
    }
}
