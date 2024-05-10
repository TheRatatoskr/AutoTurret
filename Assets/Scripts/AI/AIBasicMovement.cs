using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBasicMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Transform _currentWaypoint;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _deathTimer = 5f;

    [SerializeField] private Collider _collider;


    // Start is called before the first frame update
    void Start()
    {
        if(_navMeshAgent == null)
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            if(_navMeshAgent != null)
            {
                Debug.LogError(gameObject.name + " is missing a NavMeshAgent");

            }    
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
        MovementAnimation();
    }

    private void MoveToTarget()
    {
        if(_navMeshAgent.enabled) 
        {
            _navMeshAgent.destination = _currentWaypoint.position;
        }
    }

    private void MovementAnimation()
    { 
        Vector3 localVelocity = transform.InverseTransformDirection(_navMeshAgent.velocity);
        
        _animator.SetFloat("forwardSpeed", localVelocity.z / _navMeshAgent.speed);
    }

    public void WasShot()
    {
        _navMeshAgent.enabled = false;
        _animator.enabled = false;
        _collider.enabled = false;
        Destroy(this.gameObject, _deathTimer);
    }

    public void TakeWayPoint(Transform playerPosition)
    {
        _currentWaypoint = playerPosition;
    }
}
