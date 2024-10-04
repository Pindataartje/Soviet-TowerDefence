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
    GameObject upgradeMenu;
    Animator upgradeMenuAnim;
    TowerOptionsData towerOptionsData;
    BuildMenu buildmenu;
    TowerAi towerStats;
    public void Start()
    {
        if (towerLVL != null)
        {
            towerLVL.text = "LVL. " + timesUpgraded.ToString();
        }

        towerStats = GetComponent<TowerAi>();

        buildmanager = GameObject.FindGameObjectWithTag("BuildManager");
        buildmenu = buildmanager.GetComponent<BuildMenu>();

        upgradeMenu = GameObject.FindGameObjectWithTag("UpgradeMenu");
        towerOptionsData = upgradeMenu.GetComponent<TowerOptionsData>();

        upgradeMenuAnim = upgradeMenu.GetComponent<Animator>();
    }
    public void TowerSelect()
    {
        towerOptionsIsOpen = true;
        upgradeMenuAnim.SetInteger("UpgradeUIState", 1);

        radiusVisual.SetActive(true);
        
        SetUpgradeMenuData();

        buildmenu.upgradeMenuIsOpen = true;
    }
    public void TowerDeselect()
    {
        towerOptionsIsOpen = false;
        upgradeMenuAnim.SetInteger("UpgradeUIState", 2);

        radiusVisual.SetActive(false);

        buildmenu.upgradeMenuIsOpen = false;
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
                towerStats.RadiusUpgradeUpdateCenter(radiusBoost);

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
