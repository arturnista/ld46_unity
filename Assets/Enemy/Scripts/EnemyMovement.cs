using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyReceiveTarget
{

    [SerializeField] protected float m_Acceleration = default;
    public float Acceleration { get => m_Acceleration; }

    [SerializeField] protected float m_MoveSpeed = default;
    public float MoveSpeed { get => m_MoveSpeed; }

    [SerializeField] protected float _maxDistance;

    protected Rigidbody2D _rigidbody;

    protected Transform _target;
    protected float _targetSpeed;
    protected float _currentSpeed;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = 0f;

        m_MoveSpeed = Random.Range(m_MoveSpeed * .8f, m_MoveSpeed * 1.2f);
    }

    void Update()
    {
        if (Vector2.Distance(_target.position, transform.position) < _maxDistance)
        {
            _targetSpeed = 0f;
        }
        else
        {
            _targetSpeed = m_MoveSpeed;
        }

        _currentSpeed = Mathf.MoveTowards(_currentSpeed, _targetSpeed, m_Acceleration * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Vector2 direction = GetDirection();
        _rigidbody.MovePosition(_rigidbody.position + (direction * _currentSpeed) * Time.fixedDeltaTime);
    }

    protected virtual Vector2 GetDirection()
    {
        return (_target.position - transform.position).normalized;
    }

    public void OnReceiveTarget(Transform target)
    {
        _target = target;
    }

}
