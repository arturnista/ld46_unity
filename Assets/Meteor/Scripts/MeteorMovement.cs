using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorMovement : MonoBehaviour
{
    
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody;
    private Vector3 _moveDirection;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Vector3 targetDirection = new Vector3(
        //     Random.value > .5f ? 1f : -1f * Random.Range(2f, 4f),
        //     Random.value > .5f ? 1f : -1f * Random.Range(2f, 4f)
        // );
        Vector3 targetDirection = Vector3.zero;
        _moveDirection = Vector3.Normalize(targetDirection - transform.position);
        _rigidbody.velocity = _moveDirection * _speed;
    }

    void FixedUpdate()
    {
        _rigidbody.velocity = _moveDirection * _speed;
    }

}
