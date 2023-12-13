using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEscape : MonoBehaviour
{
    public float transitionTime = 1f;

    public Animator transition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            // Go to main menu
            StartCoroutine(LoadLevel("MainMenu"));
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
