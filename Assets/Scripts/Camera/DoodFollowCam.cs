using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoodFollowCam : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private CinemachineVirtualCameraBase cam;
    [SerializeField] private GameObject currentTarget;
    [SerializeField] private CameraControlSystem camControlSystem;

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null)
        {
            FollowRandomDood();
        }

        if (currentTarget != null && currentTarget.GetComponent<AIBasicMovement>().iAmDead)
        {
            FollowRandomDood();
        }
    }

    private void FollowRandomDood()
    {
        if(camControlSystem == null)
        {
            return;
        }

        currentTarget = spawner.SelectRandomDood();

        if (cam.Priority >= 1 && currentTarget == null)
        {
            camControlSystem.CameraCalledToSkip();
            return;
        }

        if(currentTarget == null)
        {
            return;
        }

        cam.Follow = currentTarget.transform;
        cam.LookAt = currentTarget.transform;
    }
}
