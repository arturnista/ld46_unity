﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHealth
{

    public delegate void TakeDamageHandler(float damage);
    public event TakeDamageHandler OnTakeDamage;

    public delegate void HealHandler(float healAmount);
    public event HealHandler OnHeal;

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    private SpriteRenderer _spriteRenderer;

    [SerializeField] protected GameObject _explosionPrefab;
    protected PlayerMovement _movement;
    protected PlayerRangeAttack _attack;
    protected PlayerMissileAttack _missileAttack;

    [SerializeField] protected float m_MaxHealth;
    public float MaxHealth { get => m_MaxHealth; protected set => m_MaxHealth = value; }

    protected float m_Health;
    public float Health { get => m_Health; protected set => m_Health = value; }

    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _movement = GetComponent<PlayerMovement>();
        _attack = GetComponent<PlayerRangeAttack>();
        _missileAttack = GetComponent<PlayerMissileAttack>();
        m_Health = m_MaxHealth;
    }

    public void DealDamage(float damage)
    {
        if (m_Health < 0) return;
        
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
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spriteRenderer.enabled = false;
            _movement.enabled = false;
            _attack.enabled = false;
            _missileAttack.enabled = false;
        }
    }

    public void Heal(float amount)
    {
        m_Health += amount;
        if (m_Health > m_MaxHealth) m_Health = m_MaxHealth;

        if (OnHeal != null)
        {
            OnHeal(amount);
        }
    }

}
