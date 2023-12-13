using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class PortalController : MonoBehaviour
{
    public float radius = 4f;
    public string levelName;

    public Animator transition;
    public float transitionTime = 1f;

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    internal void LoadLevel() {
        StartCoroutine(LoadLevel(levelName));
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
