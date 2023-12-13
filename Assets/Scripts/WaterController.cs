using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{

    public GameObject player;

    private Collider targetCollider;

    void Update()
    {
        // Obtain collider
        if (targetCollider == null)
            targetCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Restart level
            RestartLevel();
        }
    }

    private void RestartLevel()
    {
        // Get current scene
        UnityEngine.SceneManagement.Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

        // Load current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.name);
    }
}
