using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionDamage : MonoBehaviour
{
    
    [SerializeField] private float _damage = 15f;
    [SerializeField] private bool _destroyOnCollide = true;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyTarget")
        {
            IHealth health = collision.gameObject.GetComponent<IHealth>();
            health.DealDamage(_damage);
            
            if (_destroyOnCollide)
            {
                EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
                enemyHealth.Kill();
            }
        }
    }

}
