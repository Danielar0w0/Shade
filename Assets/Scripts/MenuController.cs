using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public CinemachineVirtualCamera currentCam;
    public CinemachineVirtualCamera nextCam;

    // Update is called once per frame
    void Update()
    {
        // Change camera if clicked
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit)) {

                // Check if hit current object
                if (hit.collider.gameObject.name == gameObject.name) {
                    ChangeCamera();
                }
            }
        }

        // Change camera if enter is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ChangeCamera();
        }
    }

    public void ChangeCamera()
    {
        // Change camera
        currentCam.Priority = 0;
        nextCam.Priority = 1;
    }
}
