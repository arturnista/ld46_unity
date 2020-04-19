using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IEnemyReceiveTarget
{
    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private GameObject _projectilePrefab = default;

    private float _fireDelay;
    private float _fireTime;

    private Camera _camera;
    private bool _isFiring;

    private Transform _target;

    void Awake()
    {
        _fireDelay = 1f / _fireRate;
        _fireTime = 0f;
    }

    public void OnReceiveTarget(Transform target)
    {
        _target = target;
    }
    
    void Update()
    {
        _fireTime += Time.deltaTime;
        if (_fireTime >= _fireDelay)
        {
            float fireDelayPerc = _fireDelay * .4f;
            _fireTime = Random.Range(-fireDelayPerc, fireDelayPerc);
            Fire();
        }
    }

    void Fire()
    {
        Vector3 direction = Vector3.Normalize(_target.position - transform.position);
        Vector3 spawnPosition = transform.position + (direction * 1f);
        ProjectileMovement projectileCreated = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtPosition(_target.position);
    }
}
