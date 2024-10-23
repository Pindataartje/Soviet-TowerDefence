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
    public int timesUpgraded = 0;
    public float damageBoost;
    public float fireRateBoost;
    public float radiusBoost;
    [Space]
    public bool isAmmunition;
    public bool onlySellable;

    [Header("UI")]
    public TMP_Text towerLVL;

    [Header("Runtime Only")]
    public bool towerOptionsIsOpen;

    GameObject buildmanager;
    GameObject buildMenuUI;
    GameObject upgradeMenuUI;
    Animator menuUIAnim;
    TowerOptionsData towerOptionsData;
    public BuildMenu buildmenu;
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
        Debug.Log("TowerSelect Function Activated");
        if(towerStats != null)
            if (towerStats.activeTower)
            {
                Debug.Log("upgrade menu opening");
                if(onlySellable)
                    upgradeMenuUI.transform.GetChild(0).gameObject.SetActive(false);

                if (!onlySellable || maxAmountOfUpgrades > timesUpgraded)
                    upgradeMenuUI.transform.GetChild(0).gameObject.SetActive(true);

                if(timesUpgraded == maxAmountOfUpgrades)
                    upgradeMenuUI.transform.GetChild(0).gameObject.SetActive(false);
                
                if(maxAmountOfUpgrades > timesUpgraded)
                    upgradeMenuUI.transform.GetChild(0).gameObject.SetActive(true);

                towerOptionsIsOpen = true;
                menuUIAnim.SetInteger("UpgradeUIState", 1);

                if(radiusVisual != null)
                    radiusVisual.SetActive(true);

                SetUpgradeMenuData();

                buildmenu.upgradeMenuIsOpen = true;

                upgradeMenuHasBeenOpened = true;
            }
    }
    public void TowerDeselect()
    {
        Debug.Log("TowerDeselect Function Activated");
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

                if (radiusVisual != null)
                    radiusVisual.SetActive(false);

                buildmenu.upgradeMenuIsOpen = false;
            }
        }
    }
    public void DestroyTower()
    {
        buildmenu.currency += sellvalue;
        buildmenu.inMapTowerBehavior.Remove(gameObject);
        buildmenu.towersInMap.Remove(transform.parent.gameObject);

        Destroy(transform.parent.gameObject);
    }
    public void UpgradeTower()
    {
        if(buildmenu.currency >= upgradeCost)
        {
            Debug.Log("you have enough money to upgrade");
            if (maxAmountOfUpgrades > timesUpgraded)
            {
                buildmenu.currency -= upgradeCost;

                timesUpgraded++;
                towerStats.damage += damageBoost;
                towerStats.fireSpeed -= fireRateBoost;

                towerStats.radius += radiusBoost;
                radiusVisual.transform.localScale += new Vector3(radiusBoost * 2, radiusBoost * 2, radiusBoost * 2);

                if(timesUpgraded < maxAmountOfUpgrades)
                {
                    sellvalue += upgradeCost;
                    upgradeCost += costBoostPerUpgrade;

                }

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
