using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TowerVignette")
        {
            other.gameObject.GetComponent<SelectedTower>().objectIsOnPath = true;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "TowerVignette")
        {
            other.gameObject.GetComponent<SelectedTower>().objectIsOnPath = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "TowerVignette")
        {
            other.gameObject.GetComponent<SelectedTower>().objectIsOnPath = false;
        }
    }
}
