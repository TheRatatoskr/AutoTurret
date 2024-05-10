using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossCameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _crossCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAI")
        {
            _crossCamera.Priority = 12;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _crossCamera.Priority = 1;
    }

}
