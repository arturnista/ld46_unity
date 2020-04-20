using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViperMovement : EnemyMovement
{
    
    private Vector3 _targetPosition;

    void Start()
    {
        StartCoroutine(FindTargetPositionCoroutine());
    }

    IEnumerator FindTargetPositionCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(.3f);
            _targetPosition = _target.position + (Vector3)Random.insideUnitCircle * 4f;
        }
    }

    protected override Vector2 GetDirection()
    {
        return (_targetPosition - transform.position).normalized;
    }

}
