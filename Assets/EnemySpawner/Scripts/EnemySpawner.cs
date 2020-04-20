using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private float _startDelayTime = 0f;
    [SerializeField] private float _spawnTime = 2f;
    [SerializeField] private EnemySpawnTable _enemies = default;

    private Camera _camera;
    private float _height;
    private float _width;

    private Coroutine _spawnCoroutine;

    void Start()
    {
        _camera = Camera.main;
        _height = _camera.orthographicSize;
        _width = (_camera.orthographicSize * _camera.aspect);
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(_startDelayTime);
        while (true)
        {
            GameObject enemyPrefab = _enemies.GetEnemyPrefab();

            Vector2 randomPosition = Random.insideUnitCircle.normalized;
            randomPosition.x *= _width * 1.5f;
            randomPosition.y *= _height * 1.5f;

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
            yield return new WaitForSeconds(_spawnTime);
        }
    }

    public void StartSpawn()
    {
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
