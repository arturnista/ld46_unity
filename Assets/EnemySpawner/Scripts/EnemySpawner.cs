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
    [Space]
    [SerializeField] private float _spawnRadius = 1.5f;
    [SerializeField] private EnemySpawnTable _enemies = default;
    [SerializeField] private GameObject _meteorPrefab = default;
    
    private float _spawnTime = 2f;

    private Camera _camera;
    private float _height;
    private float _width;

    private Coroutine _spawnCoroutine;
    private PlayerHealth _playerHealth;
    private StationRecharge _stationRecharge;
    
    private const float _startAttackingStationChange = 0.1f;
    private const float _attackingStationChangeIncrement = 0.1f;
    private float _attackingStationChange = _startAttackingStationChange;
    private int _enemiesCreated = 0;
    private float _lastMeteorSpawn = 1f;

    void Start()
    {
        _camera = Camera.main;
        _height = _camera.orthographicSize;
        _width = (_camera.orthographicSize * _camera.aspect);
    }

    public void StartSpawn()
    {
        _stationRecharge = GameObject.FindObjectOfType<StationRecharge>();
        _playerHealth = GameObject.FindObjectOfType<PlayerHealth>();

        // Force the first enemy to attack the station
        _spawnTime = _startSpawnTime;
        _attackingStationChange = 1f;
        _enemiesCreated = 0;

        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
    }

    void Update()
    {
        float charge = _stationRecharge.RechargeAmount % 20;
        if (charge < 1 && _stationRecharge.RechargeAmount > _lastMeteorSpawn)
        {
            _lastMeteorSpawn = _stationRecharge.RechargeAmount;
            Vector2 randomPosition = GetRandomSpawnPosition();
            Instantiate(_meteorPrefab, randomPosition, Quaternion.identity);
        }
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_startDelayTime);
        
        while (true)
        {
            GameObject enemyPrefab = _enemies.GetEnemyPrefab(_stationRecharge.RechargeAmount);

            Vector2 randomPosition = GetRandomSpawnPosition();

            Transform target = null;

            float random = Random.Range(0f, 1f);
            if (random < _attackingStationChange)
            {
                target = _stationRecharge.transform;
                // Force the first 3 enemies to attack the station
                if (_enemiesCreated >= 3)
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
            _attackingStationChange += _attackingStationChangeIncrement;

            EnemyTarget enemyTarget = Instantiate(enemyPrefab, randomPosition, Quaternion.identity).GetComponent<EnemyTarget>();
            if (enemyTarget)
            {
                enemyTarget.SetTarget(target);
            }
            _enemiesCreated += 1;

            float spawnTime = Random.Range(0.8f, 1.1f) * _spawnTime;
            yield return new WaitForSeconds(spawnTime);

            _spawnTime = Mathf.MoveTowards(_spawnTime, _finalSpawnTime, _accelerationSpawnTime);

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
