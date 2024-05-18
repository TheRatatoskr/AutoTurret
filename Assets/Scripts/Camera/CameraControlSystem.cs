using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This disaster of a class is supposed to control camera switching in Unity
public class CameraControlSystem : MonoBehaviour
{
    // List of cameras, marked as private but serialized so you can struggle with it in the inspector
    [SerializeField] private List<CinemachineVirtualCameraBase> cameras;

    // Index to keep track of the current camera, starting at zero because apparently that's as high as you can count
    [SerializeField] private int cameraIndex = 0;

    [SerializeField] private float timeToWait = 5f;
    private float currentWaitTimer = 0f;

    [SerializeField] private GameObject playerTimeline;
    private bool playerTimelineWasActivated =false;

    // This method is called when the script instance is being loaded, try to keep up
    private void Start()
    {
        RevertToTravelCam();
    }



    // This method is called once per frame, because apparently, you need constant supervision
    private void Update()
    {
        currentWaitTimer += Time.deltaTime;

        if (currentWaitTimer > timeToWait)
        {
            RevertToTravelCam();
        }

        // If the R key is pressed, call ChangeCameraManually with default parameters, like the special snowflake you are
        if (Input.GetKeyDown(KeyCode.R)) ChangeCameraManually();
        // If the E key is pressed, call ChangeCameraManually with reverse set to true, because you can't make up your mind
        if (Input.GetKeyDown(KeyCode.E)) ChangeCameraManually(true);

        if (Input.GetKeyDown(KeyCode.G) && !playerTimelineWasActivated)
        {
            playerTimeline.SetActive(true);
            playerTimelineWasActivated = true;

        }
        if(Input.anyKey)
        {
            currentWaitTimer = 0f;
            cameras[cameras.Count - 1].Priority = 0;
            cameras[cameraIndex].Priority = 1;
        }
    }

    private void RevertToTravelCam()
    {
        // Iterate through all cameras and set their priority to 0 (i.e., turn them off because obviously you can't handle more than one at a time)
        foreach (CinemachineVirtualCameraBase cam in cameras)
        {
            cam.Priority = 0;
        }
        // Set the first camera's priority to 1 (i.e., turn it on so you can actually see something)
        cameras[cameras.Count-1].Priority = 1;
    }

    public void CameraCalledToSkip()
    {
        ChangeCameraManually();
    }

    // This method changes the active camera manually, because automation is clearly beyond you
    private void ChangeCameraManually(bool reverse = false)
    {
        // Set the current camera's priority to 0 (i.e., turn it off because we need to switch)
        cameras[cameraIndex].Priority = 0;

        // Change the camera index based on whether reverse is true or false, try not to get confused
        if (reverse)
        {
            cameraIndex--;
        }
        else
        {
            cameraIndex++;
        }

        // If the index goes out of bounds, wrap it around to the start or end because apparently, you can't handle basic counting
        if (cameraIndex >= cameras.Count && !reverse)
        {
            cameraIndex = 0;
        }
        
        if (cameraIndex < 0)
        {
            cameraIndex = cameras.Count - 1;
        }

        // Set the new current camera's priority to 1 (i.e., turn it on so you can keep doing whatever it is you're doing)
        cameras[cameraIndex].Priority = 1;
    }
}