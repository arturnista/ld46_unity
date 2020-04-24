using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnMinions : MonoBehaviour, IBecameVisibleListener, IEnemyReceiveTarget
{
    
    [SerializeField] private float _spawnDelay = 1f;
    [SerializeField] private float _spawnTime = 1f;
    [SerializeField] private GameObject _minionPrefab = default;
    
    private Transform _target;

    public void OnReceiveTarget(Transform target)
    {
        _target = target;
    }

    public void OnBecameVisible()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_spawnDelay);
        while (true)
        {
            EnemyTarget enemyTarget = Instantiate(_minionPrefab, transform.position + (Vector3)(Random.insideUnitCircle.normalized * 2f), Quaternion.identity).GetComponent<EnemyTarget>();
            enemyTarget.SetTarget(_target);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

}
