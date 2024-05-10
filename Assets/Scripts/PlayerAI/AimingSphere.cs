using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingSphere : MonoBehaviour
{
    [SerializeField] private ShootyAI _shootyAI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyAI")
        {
            _shootyAI.AimingSphereAlarm(other.gameObject);
        }
    }
}
