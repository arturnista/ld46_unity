using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BecameVisibleDispatcher : MonoBehaviour
{
    public void OnBecameVisible()
    {
        IBecameVisibleListener[] listeners = GetComponentsInParent<IBecameVisibleListener>();
        foreach (var listener in listeners)
        {
            listener.OnBecameVisible();
        }
    }
}
