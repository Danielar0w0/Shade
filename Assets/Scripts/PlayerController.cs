using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Interactable focus;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Check for interact key (E)
        if (Input.GetKeyDown(KeyCode.E)) {

            // Get all interactable objects in radius
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 3f);

            // Check if no objects in radius
            if (hitColliders.Length == 0) {
                // Remove focus from current focus
                SetFocus(null);
            }

            bool foundInteractable = false;

            // Loop through all objects in radius
            foreach (var hitCollider in hitColliders) {

                // Check if first object is interactable
                if (hitCollider.gameObject.GetComponent<Interactable>() != null && !foundInteractable) {

                    SetFocus(hitCollider.gameObject.GetComponent<Interactable>());

                    // Stop checking for interactable objects
                    foundInteractable = true;
                }

                // Check if object is portal
                if (hitCollider.gameObject.GetComponent<PortalController>() != null) {

                    // Load level
                    hitCollider.gameObject.GetComponent<PortalController>().LoadLevel();
                }
            }
        }

        // Check for rotate key (R)
        if (Input.GetKeyDown(KeyCode.R)) {

            // Check if focus is not null
            if (focus != null) {
                // Rotate object
                focus.Rotate();
            }
        }

        // Make focus follow in front of player
        if (focus != null) {
            focus.Move(transform);
        }
    }

    void SetFocus(Interactable newFocus) {

        if (newFocus != focus) {

            // Update focus
            // Check if focus is null
            if (focus != null) {

                // Remove focus from current focus
                focus.OnDefocused();

                // Set focus to null
                focus = null;

            } else {

                // Set focus to new focus
                focus = newFocus;

                // Set new focus to focused
                focus.OnFocused();
            }
            
        } else {

            // Toggle focus off
            // Remove focus from current focus
            focus.OnDefocused();

            // Set focus to null
            focus = null;

        }
    }

    public void Fall() {
        // Make player fall
        transform.position = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);
    }
}
