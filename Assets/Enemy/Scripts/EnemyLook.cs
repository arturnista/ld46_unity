using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLook : MonoBehaviour, IEnemyReceiveTarget
{

    private Transform _target;

    public void OnReceiveTarget(Transform target)
    {
        _target = target;
    }

    void Start()
    {
    }

    void Update()
    {
        if (_target != null)
        {
            Vector2 lookDirection = (_target.position - transform.position).normalized;

            float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
        }
    }
}
