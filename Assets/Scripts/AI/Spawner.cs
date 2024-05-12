using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private GameObject _player;

    //update to use real time instead of frames plox
    [SerializeField] private float spawnWaitTime = 10f;

    //currently three but use round robin plox
    [SerializeField] private List<Transform> spawnPoints;
    private int spawnPointIndex = 0;

    [Header("**Debug Only**")]
    [SerializeField] private float _spawnTimer = 0;

    // Update is called once per frame
    void Update()
    {
        _spawnTimer += Time.deltaTime;
        SpawnDood();
    }

    private void SpawnDood()
    {
        if(_spawnTimer > spawnWaitTime)
        {
            GameObject spawnedDood = Instantiate(_enemyToSpawn, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);

            //assigns player destination to object
            //will need to update AI to read as player instead of target for the door
            spawnedDood.GetComponent<AIBasicMovement>().TakeWayPoint(_player.transform);

            //increase index, and if index is greater than count, then go back to 0
            spawnPointIndex++;
            if (spawnPointIndex > spawnPoints.Count -1) 
            { 
                spawnPointIndex = 0; 
            }

            _spawnTimer = 0;
        }

    }
}
