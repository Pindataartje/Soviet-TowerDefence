using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    public GameObject towerPrefab;

    public void SpawnTowerVignette()
    {
        Instantiate(towerPrefab);
    }
}
