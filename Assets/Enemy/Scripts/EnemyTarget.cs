using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    
    private Transform _target;
    
    public void SetTarget(Transform target)
    {
        _target = target;

        IEnemyReceiveTarget[] targetReceivers = GetComponentsInChildren<IEnemyReceiveTarget>();
        if (targetReceivers.Length > 0)
        {
            foreach (var targetReceiver in targetReceivers)
            {
                targetReceiver.OnReceiveTarget(_target);
            }
        }
    }

}
