using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerOptions : MonoBehaviour
{
    [Header("Objects")]
    public GameObject radiusVisual;

    [Header("Stats change and cost")]
    public int upgradeCost;
    public int costBoostPerUpgrade;
    [Space]
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
    GameObject buildMenuUI;
    GameObject upgradeMenuUI;
    Animator menuUIAnim;
    TowerOptionsData towerOptionsData;
    BuildMenu buildmenu;
    public TowerAi towerStats;
    public bool upgradeMenuHasBeenOpened;
    public void Start()
    {
        //towerStats = GetComponent<TowerAi>();

        buildmanager = GameObject.FindGameObjectWithTag("BuildManager");
        buildmenu = buildmanager.GetComponent<BuildMenu>();

        if(!buildmenu.isTestScene)
        {
            upgradeMenuUI = GameObject.FindGameObjectWithTag("UpgradeUI");
            towerOptionsData = upgradeMenuUI.GetComponent<TowerOptionsData>();

            buildMenuUI = GameObject.FindGameObjectWithTag("BuildUI");
            menuUIAnim = buildMenuUI.GetComponent<Animator>();
        }

        if (towerLVL != null)
        {
            towerLVL.text = "LVL. " + timesUpgraded.ToString();
        }
    }
    public void TowerSelect()
    {
        if(towerStats != null)
        {
            if (towerStats.activeTower)
            {
                Debug.Log("upgrade menu opening");
                towerOptionsIsOpen = true;
                menuUIAnim.SetInteger("UpgradeUIState", 1);

                radiusVisual.SetActive(true);

                SetUpgradeMenuData();

                buildmenu.upgradeMenuIsOpen = true;

                upgradeMenuHasBeenOpened = true;
            }
        }
    }
    public void TowerDeselect()
    {
        Debug.Log("function activated");
        //if (towerStats == null)
        //{
        //    Debug.Log("null check");
        //    return;
        //}
        if (upgradeMenuHasBeenOpened == true)
        {
            Debug.Log("upgrade menu closing");

            if (towerOptionsIsOpen == true)
            {
                towerOptionsIsOpen = false;
                menuUIAnim.SetInteger("UpgradeUIState", 2);

                radiusVisual.SetActive(false);

                buildmenu.upgradeMenuIsOpen = false;
            }
        }
    }
    public void DestroyTower()
    {
        buildmenu.currency += sellvalue;
        buildmenu.towersInMap.Remove(gameObject);
        Destroy(gameObject);
    }
    public void UpgradeTower()
    {
        if(buildmenu.currency >= upgradeCost)
        {
            Debug.Log("you have enough money to upgrade");
            if (maxAmountOfUpgrades > timesUpgraded)
            {
                timesUpgraded++;
                towerStats.damage += damageBoost;
                towerStats.fireSpeed -= fireRateBoost;

                towerStats.radius += radiusBoost;
                radiusVisual.transform.localScale += new Vector3(radiusBoost * 2, radiusBoost * 2, radiusBoost * 2);

                sellvalue += upgradeCost;
                upgradeCost += costBoostPerUpgrade;

                buildmenu.currency -= upgradeCost;

                SetUpgradeMenuData();
                if (towerLVL != null)
                {
                    towerLVL.text = "LVL. " + timesUpgraded.ToString();
                }
            }
        }
    }
    public void SetUpgradeMenuData()
    {
        towerOptionsData.towerName = towerStats.towerName;
        towerOptionsData.towerUpgradeCost = upgradeCost;
        towerOptionsData.towerSellValue = sellvalue;
        towerOptionsData.towerOptions = GetComponent<TowerOptions>();

        towerOptionsData.SetUI();
    }
    public void GetUpgradeData()
    {
        UpgradeTower();
    }
    public void GetSellData()
    {
        DestroyTower();
    }
}
