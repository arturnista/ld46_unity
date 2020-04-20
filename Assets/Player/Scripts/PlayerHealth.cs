using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{

    public delegate void TakeDamageHandler(float damage);
    public event TakeDamageHandler OnTakeDamage;

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] protected float m_MaxHealth;
    public float MaxHealth { get => m_MaxHealth; protected set => m_MaxHealth = value; }

    protected float m_Health;
    public float Health { get => m_Health; protected set => m_Health = value; }

    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_Health = m_MaxHealth;
    }

    public void DealDamage(float damage)
    {
        m_Health -= damage;
        if (m_Health > 0)
        {
            if (OnTakeDamage != null)
            {
                OnTakeDamage(damage);
            }
        }
        else
        {
            if (OnDeath != null)
            {
                OnDeath();
            }
            _spriteRenderer.enabled = false;
        }
    }

}
