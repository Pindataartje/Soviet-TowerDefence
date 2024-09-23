using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerOptions : MonoBehaviour
{
    [Header("Stats change and cost")]
    public int sellvalue;
    [Space]
    public int maxAmountOfUpgrades;
    int timesUpgraded = 0;
    public float damageBoost;
    public float fireRateBoost;
    public float radiusBoost;

    [Header("UI")]
    public TMP_Text towerLVL;

    [Header("Runtime Only")]
    public bool towerOptionsIsOpen;

    GameObject buildmanager;
    BuildMenu buildmenu;
    TowerAi towerStats;
    public void Start()
    {
        towerLVL.text = "LVL. " + timesUpgraded.ToString();

        towerStats = GetComponent<TowerAi>();

        buildmanager = GameObject.FindGameObjectWithTag("BuildManager");
        buildmenu = buildmanager.GetComponent<BuildMenu>();
    }
    public void TowerSelect()
    {
        towerOptionsIsOpen = true;
        GameObject onSelectedTowerCanvas = transform.GetChild(0).gameObject;
        onSelectedTowerCanvas.SetActive(true);
        
    }
    public void TowerDeselect()
    {
        towerOptionsIsOpen = false;
        GameObject onSelectedTowerCanvas = transform.GetChild(0).gameObject;
        onSelectedTowerCanvas.SetActive(false);
    }
    public void DestroyTower()
    {
        buildmenu.towersInMap.Remove(gameObject);
        Destroy(gameObject);
    }
    public void UpgradeTower()
    {
        if(maxAmountOfUpgrades > timesUpgraded)
        {
            timesUpgraded++;
            towerStats.damage += damageBoost;
            towerStats.fireSpeed -= fireRateBoost;
            towerStats.radius += radiusBoost;
            towerLVL.text = "LVL. " + timesUpgraded.ToString();
        }
    }
}