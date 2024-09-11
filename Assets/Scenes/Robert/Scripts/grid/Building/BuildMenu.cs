using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenu : MonoBehaviour
{
    public GameObject Grid;

    public Button[] towerButtons;
    public GameObject[] towerVignettePrefabs;

    public int selectedTowerNumber;

    public bool towerBeingPlaced = false;
    private void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {

        }
        Selecting();
    }
    public void ButtonFinder()
    {
        towerButtons[0].onClick.AddListener(delegate { TowerPicker(0); });
        towerButtons[1].onClick.AddListener(delegate { TowerPicker(1); });
        towerButtons[2].onClick.AddListener(delegate { TowerPicker(2); });
        towerButtons[3].onClick.AddListener(delegate { TowerPicker(3); });
    }
    private void TowerPicker(int towerNumber)
    {
        Instantiate(towerVignettePrefabs[towerNumber]);
        Debug.Log(towerNumber);
        selectedTowerNumber = towerNumber;
        towerBeingPlaced = true;
    }

    private void Selecting()
    {
        if (towerBeingPlaced)
        {
            Grid.SetActive(true);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.tag == "Buildable")
                {
                    Debug.Log("You Can Place");
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Grid.SetActive(false);

                        towerBeingPlaced = false;
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
