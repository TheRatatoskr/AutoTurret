using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootyAI : MonoBehaviour
{
    [SerializeField] private float shootWaitTime;

    [Header("Required Objects")]
    [SerializeField] private AimingSphere aimingSphere;
    [SerializeField] private Animator animator;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource shotSound;

    [SerializeField] private CinemachineVirtualCamera shoulderCam;
    [SerializeField] private CinemachineImpulseSource camShaker;

    [Header("Debug/View Only")]
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject currentTarget;

    private float _canShootTimer = 0;
    private bool _canShoot = false;

    private void Update()
    {
        DetermineTarget();

        _canShootTimer++;
    }

    private void DetermineTarget()
    {
        if (enemies.Count == 0)
        {
            _canShoot = false;
            //go to idle animation
            return;
        }
        currentTarget = enemies[0];
        _canShoot = true;
        ShootyMcShootyFace();
    }

    private void ShootyMcShootyFace()
    {
        if(_canShootTimer > shootWaitTime && _canShoot)
        {
            if (currentTarget != null)
            {
                transform.LookAt(currentTarget.transform.position);
                //animator.SetTrigger("isShooting");
                muzzleFlash.Play();
                camShaker.GenerateImpulse();
                shotSound.Play();
                currentTarget.GetComponent<AIBasicMovement>().WasShot();
                _canShootTimer = 0;
                enemies.Remove(currentTarget);
                currentTarget = null;
            }
        }
    }

    public void AimingSphereAlarm(GameObject detectedEnemy)
    {
        enemies.Add(detectedEnemy);
    }

}
