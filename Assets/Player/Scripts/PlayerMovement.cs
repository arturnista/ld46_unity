using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] private float m_Acceleration = default;
    public float Acceleration { get => m_Acceleration; }

    [SerializeField] private float m_MoveSpeed = default;
    public float MoveSpeed { get => m_MoveSpeed; }

    private Rigidbody2D _rigidbody;
    private Vector2 _targetVelocity;
    private Vector2 _currentVelocity;
    private Vector2 _motion;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _motion = Vector2.zero; 
        _motion.x = Input.GetAxisRaw("Horizontal");
        _motion.y = Input.GetAxisRaw("Vertical");
        
        _motion.Normalize();

        // _targetVelocity = ((transform.up * _motion.y) + (transform.right * _motion.x)) * m_MoveSpeed;
        _targetVelocity = _motion * m_MoveSpeed;
        _currentVelocity = Vector2.MoveTowards(_currentVelocity, _targetVelocity, m_Acceleration * Time.deltaTime);
    }

    void FixedUpdate()
    {
        Debug.DrawRay(transform.position, _currentVelocity.normalized * 3f, Color.yellow);
        _rigidbody.MovePosition(_rigidbody.position + _currentVelocity * Time.fixedDeltaTime);
    }

}
