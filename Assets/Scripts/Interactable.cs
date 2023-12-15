using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;
    public bool isFocus = false;

    // Default material
    public Material defaultMaterial;

    // Material when focused
    public Material focusedMaterial;

    // Is object rotatable
    public bool rotatable = true;

    // Is object movable
    public bool movable = true;

    internal void OnDefocused()
    {
        isFocus = false;

        // Change material to default material
        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().material = defaultMaterial;

        // Change material of all children
        foreach (Transform child in transform) {
            if (child.GetComponent<Renderer>() != null)
                child.GetComponent<Renderer>().material = defaultMaterial;
        }
    }

    internal void OnFocused()
    {
        isFocus = true;

        // Change material to focused material
        if (GetComponent<Renderer>() != null)
            GetComponent<Renderer>().material = focusedMaterial;

        // Change material of all children
        foreach (Transform child in transform) {
            if (child.GetComponent<Renderer>() != null)
                child.GetComponent<Renderer>().material = focusedMaterial;
        }
    }

    internal void Rotate()
    {
        if (!rotatable) return;
        // Rotate object
        transform.Rotate(0, 0, 45);
    }

    internal void Move(Transform target)
    {
        if (!movable) return;
        // Move object
        transform.position = target.position + target.forward * radius + target.up * 1.2f;
    }


    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
