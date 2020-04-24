using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHomingMovement : ProjectileMovement
{

    [SerializeField] private float _startAngularSpeed = 10f;
    [SerializeField] private float _angularAcceleration = 1f;

    private Transform _target;

    private float _angle = 0f;
    private float _angularSpeed = 10f;

    public override void ShootAtDirection(Vector3 direction)
    {
        base.ShootAtDirection(direction);

        FindEnemy();

        _angularSpeed = _startAngularSpeed;
        _angle = transform.eulerAngles.z;
    }

    void Update()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 targetDirection = Vector3.Normalize(_target.position - transform.position);
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        targetAngle -= 90f;

        _angle = Mathf.MoveTowardsAngle(_angle, targetAngle, _angularSpeed * Time.deltaTime);
        _angularSpeed += _angularAcceleration * Time.deltaTime;

        transform.rotation = Quaternion.Euler(0f, 0f, _angle);
        _rigidbody.velocity = transform.up * _speed;

        Debug.DrawLine(transform.position, _target.position, Color.red);
    }

    void FindEnemy()
    {
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        if (enemies.Count > 0)
        {
            GameObject target = enemies[Random.Range(0, enemies.Count)];
            _target = target.transform;
        }
    }

}
