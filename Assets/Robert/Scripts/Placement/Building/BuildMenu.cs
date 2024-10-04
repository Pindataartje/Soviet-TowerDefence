using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public int currency;
    public TMP_Text currencyText;

    [Header("Dev tools")]
    public bool enableDevTools;

    [Header("Runtime Only")]
    public bool upgradeMenuIsOpen = false;
    public bool buildMenuIsOpen = false;
    bool towerBeingPlaced = false;

    private void Update()
    {
        currencyText.text = currency.ToString();
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (!buildMenuIsOpen && !upgradeMenuIsOpen)
            {
                buildMenuUI.SetActive(true);
                buildMenuIsOpen = true;
            }
            else
            {
                if (!towerBeingPlaced)
                {
                    buildMenuUI.SetActive(false);
                    buildMenuIsOpen = false;
                }
            }
        }

        ATowerMenuIsOpen();

        Selecting();

        if (enableDevTools)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                currency += 1000;
            }
        }

    }
    #region TowerButtons
    public void TowerOne()
    {
        if(currency >= towers[0].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[0]);

            selectedTowerNumber = 0;
            towerBeingPlaced = true;
        }
    }
    public void TowerTwo()
    {
        if (currency >= towers[1].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[1]);

            selectedTowerNumber = 1;
            towerBeingPlaced = true;
        }
    }
    public void TowerThree()
    {
        if (currency >= towers[2].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[2]);

            selectedTowerNumber = 2;
            towerBeingPlaced = true;
        }
    }
    public void TowerFour()
    {
        if (currency >= towers[3].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[3]);

            selectedTowerNumber = 3;
            towerBeingPlaced = true;
        }
    }
    #endregion
    #region Selecting
    private void Selecting()
    {
        
        if (towerBeingPlaced)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                towerBeingPlaced = false;

                Destroy(selectedVignette);
            }
            if(towerIsNotOnPath)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject placedTower = Instantiate(towers[selectedTowerNumber], selectedVignette.transform.position, selectedVignette.transform.rotation);
                    towersInMap.Add(placedTower);
                    currency -= towers[selectedTowerNumber].GetComponent<TowerAi>().cost;

                    towerBeingPlaced = false;

                    buildMenuUI.SetActive(false);
                    buildMenuIsOpen = false;

                    Destroy(selectedVignette);
                }
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
                    if(!buildMenuIsOpen)
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
}
