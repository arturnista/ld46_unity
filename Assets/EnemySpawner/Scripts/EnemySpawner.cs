using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float _startDelayTime = 0f;
    [Space]
    [SerializeField] private float _startSpawnTime = 2f;
    [SerializeField] private float _finalSpawnTime = 1.3f;
    [SerializeField] private float _accelerationSpawnTime = 0.3f;
    [SerializeField] private bool _enemiesTargetStation = false;
    [Space]
    [SerializeField] private float _spawnRadius = 1.5f;
    [SerializeField] private EnemySpawnTable _enemies = default;
    [Space]
    [SerializeField] private GameObject _meteorPrefab = default;
    
    private float _spawnTime = 2f;

    private Camera _camera;
    private float _height;
    private float _width;

    private Coroutine _spawnCoroutine;
    private PlayerHealth _playerHealth;
    private StationRecharge _stationRecharge;
    
    private const float _startAttackingStationChange = 0.1f;
    private const float _attackingStationChangeIncrement = 0.07f;
    private float _attackingStationChange = _startAttackingStationChange;
    private int _enemiesCreated = 0;
    private const float _firstMeteorSpawnAfter = 30f;
    private float _lastMeteorSpawn = _firstMeteorSpawnAfter;
    private int _meteorIndex = 0;
    private GameObject _meteorCreated;

    public void StartSpawn()
    {
        _camera = Camera.main;
        _height = _camera.orthographicSize;
        _width = (_camera.orthographicSize * _camera.aspect);

        _stationRecharge = GameObject.FindObjectOfType<StationRecharge>();
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        // Force the first enemy to attack the station
        _spawnTime = _startSpawnTime;
        _attackingStationChange = 1f;
        _enemiesCreated = 0;
        _meteorIndex = 0;
        _lastMeteorSpawn = _firstMeteorSpawnAfter;

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    void Update()
    {
        if (_stationRecharge == null) return;
        float charge = _stationRecharge.RechargeAmount % 50;
        if (charge < 1 && _stationRecharge.RechargeAmount > _lastMeteorSpawn)
        {
            _lastMeteorSpawn = _stationRecharge.RechargeAmount + 5f;
            Vector2 randomPosition = GetRandomSpawnPosition();
            _meteorCreated = Instantiate(_meteorPrefab, randomPosition, Quaternion.identity);
            _meteorIndex += 1;
        }
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_startDelayTime);
        
        while (true)
        {
            while (_meteorCreated != null)
            {
                yield return new WaitForSeconds(.1f);
            }

            GameObject enemyPrefab = _enemies.GetEnemyPrefab(_stationRecharge.RechargeAmount);

            Vector2 randomPosition = GetRandomSpawnPosition();

            EnemyTarget enemyTarget = Instantiate(enemyPrefab, randomPosition, Quaternion.identity).GetComponent<EnemyTarget>();
            SetEnemyTarget(enemyTarget);

            _enemiesCreated += 1;

            float spawnTime = Random.Range(0.8f, 1.1f) * _spawnTime;
            yield return new WaitForSeconds(spawnTime);

            _spawnTime = Mathf.MoveTowards(_spawnTime, _finalSpawnTime, _accelerationSpawnTime);
        }
    }

    void SetEnemyTarget(EnemyTarget enemyTarget)
    {
        if (!_enemiesTargetStation)
        {

            if (enemyTarget)
            {
                enemyTarget.SetTarget(_playerHealth.transform);
            }

        }
        else
        {
            
            if (enemyTarget)
            {
                Transform target = null;

                float random = Random.Range(0f, 1f);
                if (random < _attackingStationChange)
                {
                    target = _stationRecharge.transform;
                    // Force the first 3 enemies to attack the station
                    if (_enemiesCreated >= 2)
                    {
                        _attackingStationChange = _startAttackingStationChange;
                    }
                    else
                    {
                        _attackingStationChange = 1f;
                    }
                }
                else
                {
                    target = _playerHealth.transform;
                }
                enemyTarget.SetTarget(target);
            }

            _attackingStationChange += _attackingStationChangeIncrement;

        }

    }

    Vector2 GetRandomSpawnPosition()
    {
        Vector2 randomPosition = Random.insideUnitCircle.normalized;
        randomPosition.x *= _width * _spawnRadius;
        randomPosition.y *= _height * _spawnRadius;
        return randomPosition;
    }

    public void StopSpawn()
    {
        StopCoroutine(_spawnCoroutine);
    }

    public void KillAllEnemies()
    {
        EnemyHealth[] enemyHealth = GameObject.FindObjectsOfType<EnemyHealth>();
        foreach (var health in enemyHealth)
        {
            health.Kill();
        }

    }

}
