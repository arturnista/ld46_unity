using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : ProjectileAttack
{
    
    [SerializeField] private float _radius;

    private LayerMask _collisionLayerMask;
    private Collider2D[] _collisionResults;

    void Awake()
    {
        _collisionResults = new Collider2D[5];
        ConstructLayerMask();
    }

    void ConstructLayerMask()
    {
        _collisionLayerMask = 0;
    
        int myLayer = gameObject.layer;
        for (int i = 0; i < 32; i++) {
            if (!Physics2D.GetIgnoreLayerCollision(myLayer, i))  {
                _collisionLayerMask = _collisionLayerMask | 1 << i;
            }
        }
    }

    protected override void Collide(Collider2D collide)
    {
        int results = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _collisionResults, _collisionLayerMask);

        for (int i = 0; i < results; i++)
        {
            IHealth health = _collisionResults[i].GetComponent<IHealth>();
            if (health != null)
            {
                health.DealDamage(_damage);
            }
        }

        if (_explosionPrefab != null)
        {
            GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            explosion.transform.localScale = Vector3.one * _radius * 2f;
        }

        Destroy(gameObject);
    }

}
