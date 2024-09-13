using System.Collections;
using UnityEngine;

public class DelayedObjectSwitching : MonoBehaviour
{
    public float delayTime;
    private GameObject objectToActivate;

    public void SetObjectToActivate(GameObject obj)
    {
        objectToActivate = obj;
        Debug.Log("Object to activate set to: " + obj.name);
    }

    public void ActivateWithStoredDelay()
    {
        if (objectToActivate != null)
        {
            Debug.Log("Starting coroutine to activate " + objectToActivate.name + " after " + delayTime + " seconds.");
            StartCoroutine(ActivateWithDelay(objectToActivate, delayTime));
        }
        else
        {
            Debug.LogWarning("No object set to activate.");
        }
    }

    private IEnumerator ActivateWithDelay(GameObject objectToActivate, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Activating object: " + objectToActivate.name);
        objectToActivate.SetActive(true);
    }
}