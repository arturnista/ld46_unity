using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHealth
{

    public delegate void DeathHandler();
    public event DeathHandler OnDeath;

    [SerializeField] protected float m_MaxHealth;
    public float MaxHealth { get => m_MaxHealth; set => m_MaxHealth = value; }

    protected float m_Health;
    public float Health { get => m_Health; set => m_Health = value; }

    [SerializeField] protected Color _damageColor;
    [SerializeField] protected GameObject _explosionPrefab;

    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;

        m_Health = m_MaxHealth;
    }

    public void DealDamage(float damage)
    {
        m_Health -= damage;
        if (m_Health <= 0)
        {
            Kill();
        }
        else
        {
            StartCoroutine(DamageCoroutine());
        }
    }

    IEnumerator DamageCoroutine()
    {
        for (int i = 0; i < 5; i++)
        {
            _spriteRenderer.color = i % 2 == 0 ? _damageColor : _originalColor;
            yield return new WaitForSeconds(.05f);
        }

        _spriteRenderer.color = _originalColor;
    }

    public void Kill()
    {
        if (OnDeath != null)
        {
            OnDeath();
        }
        
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
