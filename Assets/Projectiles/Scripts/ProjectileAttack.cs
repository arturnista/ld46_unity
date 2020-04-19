using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    
    [SerializeField] protected float _damage;
    [SerializeField] protected GameObject _explosionPrefab = default;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Collide(collider);
    }

    protected virtual void Collide(Collider2D collide)
    {
        IHealth health = collide.GetComponent<IHealth>();
        if (health != null)
        {
            health.DealDamage(_damage);
        }

        if (_explosionPrefab != null)
        {
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

}
