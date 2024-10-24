using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllowedToPlace : MonoBehaviour
{
    public bool isBarbedWire;

    bool obstructionOnPath;
    public void OnTriggerEnter(Collider other)
    {
        if (!isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
        }
        
        if(isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "Enemy" || other.tag == "Wire")
            {
                obstructionOnPath = true;
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
            if (!obstructionOnPath)
            {
                if (other.tag == "EnemyPath")
                {
                    GetComponent<SelectedTower>().objectIsOnPath = false;
                }
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

        if (isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "Enemy" || other.tag == "Wire")
            {
                obstructionOnPath = true;
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
            if (!obstructionOnPath)
            {
                if (other.tag == "EnemyPath")
                {
                    GetComponent<SelectedTower>().objectIsOnPath = false;
                }
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

        if (isBarbedWire)
        {
            if (other.tag == "NonPlaceable" || other.tag == "Tower" || other.tag == "Enemy" || other.tag == "Wire")
            {
                obstructionOnPath = false;
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
            if (other.tag == "EnemyPath")
            {
                GetComponent<SelectedTower>().objectIsOnPath = true;
            }
        }
    }
}
