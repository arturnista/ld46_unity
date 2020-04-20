using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileProjectile : ProjectileMovement
{
    
    private Transform _target;
    private float _targetAngle;
    private float _currentAngle;
    private float _acceleration;

    void Start()
    {
        EnemyHealth[] healths = GameObject.FindObjectsOfType<EnemyHealth>();
        float distance = 0f;

        foreach (var health in healths)
        {
            if (_target == null)
            {
                distance = SetTarget(health.transform);
            }
            else if (distance > Vector3.Distance(health.transform.position, transform.position))
            {
                distance = SetTarget(health.transform);
            }
        }

        _acceleration = 100f;
        _currentAngle = transform.eulerAngles.z;
    }

    float SetTarget(Transform target)
    {
        _target = target;
        return Vector3.Distance(_target.position, transform.position);
    }

    void Update()
    {
        if (_target != null)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _targetAngle -= 90f;
            
            _currentAngle = Mathf.MoveTowardsAngle(_currentAngle, _targetAngle, _acceleration * Time.deltaTime);
            _acceleration += 65f * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0f, 0f, _currentAngle);
            _rigidbody.velocity = transform.up * _speed;
        }
    }

}
