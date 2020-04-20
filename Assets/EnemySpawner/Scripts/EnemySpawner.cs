using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float _startDelayTime = 0f;
    [SerializeField] private float _startSpawnTime = 2f;
    [SerializeField] private float _minSpawnTime = 1.3f;
    [SerializeField] private EnemySpawnTable _enemies = default;
    
    private float _spawnTime = 2f;
    private bool _isActive = false;

    private Camera _camera;
    private float _height;
    private float _width;

    private Coroutine _spawnCoroutine;
    private StationRecharge _stationRecharge;

    void Start()
    {
        _camera = Camera.main;
        _height = _camera.orthographicSize;
        _width = (_camera.orthographicSize * _camera.aspect);

        _spawnTime = _startDelayTime;
    }

    void Update()
    {
        if (_isActive)
        {
            _spawnTime = Mathf.MoveTowards(_spawnTime, _minSpawnTime, .3f * Time.deltaTime);
        }
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_startDelayTime);
        _isActive = true;
        while (true)
        {
            GameObject enemyPrefab = _enemies.GetEnemyPrefab(_stationRecharge.RechargeAmount);

            Vector2 randomPosition = Random.insideUnitCircle.normalized;
            randomPosition.x *= _width * 1.5f;
            randomPosition.y *= _height * 1.5f;

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void StartSpawn()
    {
        _stationRecharge = GameObject.FindObjectOfType<StationRecharge>();
        _spawnCoroutine = StartCoroutine(SpawnCoroutine());
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
