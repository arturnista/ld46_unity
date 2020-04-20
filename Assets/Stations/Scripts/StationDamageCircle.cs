using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationDamageCircle : MonoBehaviour
{
    
    [SerializeField] private float _speed = 10f;

    private float _size = .25f;

    void Update()
    {
        transform.localScale = Vector3.one * _size;
        _size += _speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyHealth health = collider.gameObject.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.Kill();
        }
    }

}
