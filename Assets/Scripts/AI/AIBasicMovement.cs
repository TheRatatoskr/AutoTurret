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

    [SerializeField] private List<AudioClip> deathSounds;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private Collider _collider;
    [SerializeField] private Rigidbody rb;
    private Rigidbody[] ragdollBody;

    public bool iAmDead = false;
    public Spawner spawner;

    private void Awake()
    {
        ragdollBody = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody body in ragdollBody)
        {
            body.isKinematic = true;
        }
    }
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
        iAmDead = true;

        spawner.IGotShot(gameObject);

        _navMeshAgent.enabled = false;
        _animator.enabled = false;
        foreach (Rigidbody body in ragdollBody)
        {
            body.isKinematic = false;
            body.AddForce(Vector3.right * 300f + Vector3.up * 200f);
        }
        _collider.enabled = false;
        
        _audioSource.clip = deathSounds[Random.Range(0, deathSounds.Count-1)];
        _audioSource.Play();
        Destroy(this.gameObject, _deathTimer);
    }

    public void TakeWayPoint(Transform playerPosition)
    {
        _currentWaypoint = playerPosition;
    }
}
