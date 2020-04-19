using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IEnemyReceiveTarget
{

    [SerializeField] private float m_Acceleration = default;
    public float Acceleration { get => m_Acceleration; }

    [SerializeField] private float m_MoveSpeed = default;
    public float MoveSpeed { get => m_MoveSpeed; }

    [SerializeField] private float _maxDistance;

    private Rigidbody2D _rigidbody;

    private Transform _target;
    private float _targetSpeed;
    private float _currentSpeed;
    
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
        Vector2 direction = (_target.position - transform.position).normalized;
        _rigidbody.MovePosition(_rigidbody.position + (direction * _currentSpeed) * Time.fixedDeltaTime);
    }

    public void OnReceiveTarget(Transform target)
    {
        _target = target;
    }

}
