using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMinions : MonoBehaviour
{
    
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private float _spawnTime = 1f;
    [SerializeField] private GameObject _minionPrefab = default;

    void Start()
    {
        
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        while (true)
        {
            Instantiate(_minionPrefab, transform.position + (Vector3)Random.insideUnitCircle, Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

}
