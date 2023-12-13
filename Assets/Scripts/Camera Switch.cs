using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{

    public bool isCamera1Active = true;
    public CinemachineFreeLook camera1;
    public CinemachineVirtualCamera camera2;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        if (isCamera1Active)
        {
            camera1.Priority = 0;
            camera2.Priority = 1;

            isCamera1Active = false;
        }
        else
        {
            camera1.Priority = 1;
            camera2.Priority = 0;

            isCamera1Active = true;
        }
    }
}
