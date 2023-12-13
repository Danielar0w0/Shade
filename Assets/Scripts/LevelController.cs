using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Cinemachine;

public class LevelController : MonoBehaviour
{

    public string levelName;

    public Animator transition;
    public float transitionTime = 1f;

    void Update() {
        
        // On click
        if (Input.GetMouseButtonDown(0)) {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit)) {

                // Check if hit current object
                if (hit.collider.gameObject.name == gameObject.name) {
                    StartCoroutine(LoadLevel(levelName));
                }
            }
        }
    }

    IEnumerator LoadLevel(string levelName) {
        
        // Play animation
        transition.SetTrigger("Start");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelName);
    }
}
