using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualShootControls : MonoBehaviour
{
    [SerializeField] private float rayLength;
    [SerializeField] private Vector3 sourceAdjust;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioSource shotSound;

    [SerializeField] private CinemachineVirtualCamera shoulderCam;
    [SerializeField] private CinemachineImpulseSource camShaker;

    [SerializeField] private int endingHitTotal = 15;
    private int hitCounter = 0;

    [SerializeField] private GameObject exitTimeline;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + sourceAdjust, transform.forward * rayLength, Color.magenta);
        if(Input.GetMouseButtonDown(0))
        {
            camShaker.GenerateImpulse();
            muzzleFlash.Play();
            shotSound.Play();

            Ray ray = new Ray(transform.position + sourceAdjust, transform.forward * rayLength);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength))
            {
                if(hit.collider.tag == "EnemyAI")
                {
                    hit.collider.gameObject.GetComponent<AIBasicMovement>().WasShot();
                    hitCounter++;
                    if(hitCounter >= endingHitTotal)
                    {
                        exitTimeline.SetActive(true);
                    }
                }
            }
        }
    }
}
