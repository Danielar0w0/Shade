using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    // Get Player
    public GameObject cam;

    // Update is called once per frame
    void Update()
    {
        // Face player
        transform.LookAt(new Vector3(cam.transform.position.x, transform.position.y, cam.transform.position.z));
        
        // Flip text
        transform.Rotate(0, 180, 0);
    }
}
