using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMinions : MonoBehaviour, IBecameVisibleListener
{
    
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private float _spawnTime = 1f;
    [SerializeField] private GameObject _minionPrefab = default;

    public void OnBecameVisible()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        while (true)
        {
            Instantiate(_minionPrefab, transform.position + (Vector3)(Random.insideUnitCircle.normalized * 2f), Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

}
