using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerOptionsData : MonoBehaviour
{
    [Header("Data")]
    public bool HasHighLight;
    public ButtonHighlight[] highLights;
    [Space]
    public string towerName;
    public int towerUpgradeCost;
    public int towerSellValue;

    [Header("UI")]
    public TMP_Text towerNameUI;
    public TMP_Text towerUpgradeCostUI;
    public TMP_Text towerSellValueUI;

    [Header("Other (Runtime only)")]
    public TowerOptions towerOptions;

    public void SetUI()
    {
        towerNameUI.text = towerName;
        towerUpgradeCostUI.text = towerUpgradeCost.ToString();
        towerSellValueUI.text = towerSellValue.ToString();
        if(HasHighLight)
        {
            foreach(ButtonHighlight highlight in highLights)
            {
                if(highlight.isUpgradeButton)
                highlight.towerUpgradeCostUI.text = towerUpgradeCost.ToString();

                if(highlight.isSellButton)
                highlight.towerSellValueUI.text = towerSellValue.ToString();
            }
        }
    }
    public void UpgradeTowerButton()
    {
        towerOptions.GetUpgradeData();
    }
    public void SellTowerButton()
    {
        towerOptions.GetSellData();
    }
}
