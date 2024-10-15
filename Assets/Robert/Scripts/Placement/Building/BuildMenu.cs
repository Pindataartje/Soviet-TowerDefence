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

    public List<GameObject> towerBehavior = new List<GameObject>();
    public List<GameObject> inMapTowerBehavior = new List<GameObject>();

    [Header("Towers Placed (Only in runtime)")]
    public List<GameObject> towersInMap = new List<GameObject>();
    public bool towerIsNotOnPath = true;
    public float waitTimeToBuildAgain;

    GameObject onSelectedTowerCanvas;
    int selectedTowerNumber;

    [Header("UI")]
    public GameObject buildMenuUI;

    public int currency;
    public TMP_Text currencyText;

    [Header("Dev tools")]
    public bool enableDevTools;
    public bool isTestScene;

    [Header("Runtime Only")]
    public bool upgradeMenuIsOpen = false;
    public bool buildMenuIsOpen = false;
    public bool towerBeingPlaced = false;
    public bool canBuildAgain = true;
    Animator buildUIAnim;
    GameObject buildUIObject;

    private void Start()
    {
        if(!isTestScene)
        {
            buildUIObject = GameObject.FindGameObjectWithTag("BuildUI");
            buildUIAnim = buildUIObject.GetComponent<Animator>();
        }
        
        foreach(GameObject tower in towers)
        {
            towerBehavior.Add(tower.transform.GetChild(0).gameObject);
        }
    }
    private void Update()
    {
        currencyText.text = currency.ToString();
        if(Input.GetKeyDown(KeyCode.B))
        {
            if (!buildMenuIsOpen && !upgradeMenuIsOpen)
            {
                if (!isTestScene)
                    buildUIAnim.SetInteger("BuildUIState", 1);
                else
                    buildMenuUI.SetActive(true);
                
                buildMenuIsOpen = true;
            }
            else
            {
                if (!towerBeingPlaced)
                {
                    if (!isTestScene)
                        buildUIAnim.SetInteger("BuildUIState", 2);
                    else
                        buildMenuUI.SetActive(false);

                    buildMenuIsOpen = false;
                }
            }
        }

        Selecting();

        if (enableDevTools)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                currency += 1000;
            }
        }
        if(!buildMenuIsOpen && canBuildAgain)
        {
            ATowerMenuIsOpen();
        }
    }
    #region TowerButtons
    public void TowerOne()
    {
        if(currency >= towerBehavior[0].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[0]);

            selectedTowerNumber = 0;
            towerBeingPlaced = true;
        }
    }
    public void TowerTwo()
    {
        if (currency >= towerBehavior[1].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[1]);

            selectedTowerNumber = 1;
            towerBeingPlaced = true;
        }
    }
    public void TowerThree()
    {
        if (currency >= towerBehavior[2].GetComponent<TowerAi>().cost)
        {
            selectedVignette = Instantiate(towerVignettePrefabs[2]);

            selectedTowerNumber = 2;
            towerBeingPlaced = true;
        }
    }
    public void TowerFour()
    {
        if (currency >= towerBehavior[3].GetComponent<TowerAi>().cost)
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
            if (!isTestScene)
                buildUIAnim.SetInteger("BuildUIState", 2);
            else
                buildMenuUI.SetActive(false);

            buildMenuIsOpen = false;

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                towerBeingPlaced = false;

                if (!isTestScene)
                    buildUIAnim.SetInteger("BuildUIState", 1);
                else
                    buildMenuUI.SetActive(true);

                buildMenuIsOpen = true;

                Destroy(selectedVignette);
            }
            if(towerIsNotOnPath)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartCoroutine(WaitToBuildAgain(waitTimeToBuildAgain));
                    canBuildAgain = false;

                    GameObject placedTower = Instantiate(towers[selectedTowerNumber], selectedVignette.transform.position, selectedVignette.transform.rotation);
                    towersInMap.Add(placedTower);
                    currency -= towerBehavior[selectedTowerNumber].GetComponent<TowerAi>().cost;

                    towerBeingPlaced = false;

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
                if(hit.collider is MeshCollider)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if (!buildMenuIsOpen)
                        {
                            foreach (GameObject tower in towersInMap)
                            {
                                inMapTowerBehavior.Add(tower.transform.GetChild(0).gameObject);

                                foreach (GameObject behavior in inMapTowerBehavior)
                                {
                                    behavior.GetComponent<TowerOptions>().TowerDeselect();
                                    if (!behavior.GetComponent<TowerOptions>().towerOptionsIsOpen)
                                    {
                                        hit.transform.GetChild(0).GetComponent<TowerOptions>().TowerSelect();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    foreach(GameObject tower in towersInMap)
                    {
                        inMapTowerBehavior.Add(tower.transform.GetChild(0).gameObject);

                        foreach(GameObject behavior in inMapTowerBehavior)
                        {
                            behavior.GetComponent<TowerOptions>().TowerDeselect();
                        }
                    }
                }
            }
        }
    }
    public IEnumerator WaitToBuildAgain(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        canBuildAgain = true;
    }
}
