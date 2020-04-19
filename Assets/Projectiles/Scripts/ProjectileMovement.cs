using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    
    [SerializeField] protected float _speed;

    protected Rigidbody2D _rigidbody;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ShootAtPosition(Vector3 position)
    {
        Vector3 direction = (position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        _rigidbody.velocity = transform.up * _speed;
    }

}
