using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileProjectile : ProjectileMovement
{
    [SerializeField] private GameObject _targetPrefab;
    private GameObject _targetCreated;
    private Vector3 _targetPosition;
    private ExplosionProjectile _explosion;

    void Start()
    {
        _explosion = GetComponent<ExplosionProjectile>();
    }

    public override void ShootAtPosition(Vector3 position)
    {
        base.ShootAtPosition(position);
        _targetPosition = position;
        _targetCreated = Instantiate(_targetPrefab, _targetPosition, Quaternion.identity);
    }

    void Update()
    {
        if (Vector3.Distance(_targetPosition, transform.position) < 0.2f)
        {
            _explosion.Explode(null);
            Destroy(_targetCreated);
        }
    }

}
