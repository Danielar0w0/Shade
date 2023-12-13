using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCollider : MonoBehaviour
{

    public GameObject interactableObj;
    private Collider targetCollider;

    private bool isTriggered = false;

    void Update()
    {
        // Obtain collider
        if (targetCollider == null)
            targetCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable") && !isTriggered)
        {
            interactableObj.GetComponent<InteractiveShadows>().isTriggered = true;
            isTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Interactable") && isTriggered)
        {
            interactableObj.GetComponent<InteractiveShadows>().isTriggered = false;
            isTriggered = false;
        }
    }
}
