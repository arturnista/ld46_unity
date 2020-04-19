using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    
    Transform _target;

    void Start()
    {
        _target = GameObject.FindObjectOfType<PlayerMovement>().transform;

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
