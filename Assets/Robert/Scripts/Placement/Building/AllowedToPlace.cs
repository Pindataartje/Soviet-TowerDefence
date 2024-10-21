using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowedToPlace : MonoBehaviour
{
    public bool isBarbedWire;
    public void OnTriggerEnter(Collider other)
    {
        if (!isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
        }
        else
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "Enemy" || other.tag == "Wire")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
            else if (other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = false;
            }
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (!isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
        }
        else
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "Enemy" || other.tag == "Wire")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
            else if (other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = false;
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (!isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = false;
            }
        }
        else
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "Enemy" || other.tag == "Wire")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
            else if (other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
        }
    }
}
