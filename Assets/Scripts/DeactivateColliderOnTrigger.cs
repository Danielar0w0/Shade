using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateColliderOnTrigger : MonoBehaviour
{
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
        if (other.CompareTag("Player") && !isTriggered)
        {

            // Check if Player Focus is None
            if (other.GetComponent<PlayerController>().focus == null)
            {
                // Check if Glass Floor exists
                if (GameObject.Find("Glass Floor"))
                    GameObject.Find("Glass Floor").GetComponent<Collider>().enabled = false;

                isTriggered = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && isTriggered)
        {
            if (GameObject.Find("Glass Floor"))
                GameObject.Find("Glass Floor").GetComponent<Collider>().enabled = true;

            isTriggered = false;
        }
    }
}
