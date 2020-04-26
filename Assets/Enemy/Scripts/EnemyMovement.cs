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
    [SerializeField] protected float _refreshDirectionDelay = .5f;

    protected Rigidbody2D _rigidbody;

    protected Transform _target;
    protected float _targetSpeed;
    protected float _currentSpeed;
    protected Vector2 _lastDirection;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentSpeed = 0f;

        m_MoveSpeed = Random.Range(m_MoveSpeed * .8f, m_MoveSpeed * 1.2f);
    }

    void Start()
    {
        StartCoroutine(DirectionCoroutine());
    }

    void Update()
    {
        if (_target == null) return;
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
        if (_target == null) return;
        Vector2 direction = GetDirection();
        _rigidbody.MovePosition(_rigidbody.position + (direction * _currentSpeed) * Time.fixedDeltaTime);
    }

    protected virtual Vector2 GetDirection()
    {
        return _lastDirection;
    }

    IEnumerator DirectionCoroutine()
    {
        while (true)
        {
            Vector3 targetDiff = _target.position - transform.position;
            Vector3 targetDirection = targetDiff.normalized;
            
            Debug.DrawLine(transform.position, _target.position, Color.red, 1f);
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, .5f, targetDirection, targetDiff.magnitude - 3f, 1 << LayerMask.NameToLayer("Station"));
            // RaycastHit2D[] hits = Physics2D.LinecastAll(transform.position, _target.position, Physics2D.GetLayerCollisionMask(gameObject.layer));
            if (hits.Length > 0)
            {
                Vector3 direction = Vector3.zero;

                Vector3 right = Vector3.Cross(Vector3.forward, targetDirection);
                Vector3 targetPositionRight = hits[0].transform.position + right * 4f;
                Vector3 targetPositionLeft = hits[0].transform.position - right * 4f;
                if (Vector2.Distance(targetPositionRight, transform.position) < Vector2.Distance(targetPositionLeft, transform.position))
                {
                    direction = (targetPositionRight - transform.position).normalized;
                    Debug.DrawLine(transform.position, targetPositionRight, Color.magenta, .2f);
                }
                {
                    direction = (targetPositionLeft - transform.position).normalized;
                    Debug.DrawLine(transform.position, targetPositionLeft, Color.magenta, .2f);
                }
                Debug.DrawLine(transform.position, hits[0].transform.position, Color.yellow, .2f);
                _lastDirection = direction;
            }
            else
            {
                _lastDirection = targetDirection;
            }

            yield return new WaitForSeconds(_refreshDirectionDelay);
        }
    }

    public void OnReceiveTarget(Transform target)
    {
        _target = target;
    }

}
