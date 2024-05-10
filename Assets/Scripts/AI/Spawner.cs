using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemyToSpawn;
    [SerializeField] private GameObject _player;

    [SerializeField] private float spawnWaitTime = 10f;

    private float _spawnTimer = 0;

    // Update is called once per frame
    void Update()
    {
        _spawnTimer++;
        SpawnDood();
    }

    private void SpawnDood()
    {
        if(_spawnTimer > spawnWaitTime)
        {
            GameObject spawnedDood = Instantiate(_enemyToSpawn, transform.position, Quaternion.identity);

            spawnedDood.GetComponent<AIBasicMovement>().TakeWayPoint(_player.transform);

            _spawnTimer = 0;
        }

    }
}
