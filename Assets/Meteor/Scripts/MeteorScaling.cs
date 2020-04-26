using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScaling : MonoBehaviour
{
    
    private EnemyHealth _health;

    void Awake()
    {
        _health = GetComponent<EnemyHealth>();
    }

    public void Scale(int index)
    {
        _health.MaxHealth = _health.MaxHealth + (10f * index);
        _health.Health = _health.MaxHealth;
        transform.localScale = Vector3.one * ((index * .3f) + .8f);
    }

}
