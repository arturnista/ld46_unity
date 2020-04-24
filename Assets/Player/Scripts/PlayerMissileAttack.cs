using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileAttack : MonoBehaviour
{
    
    [SerializeField] private GameObject _missilePrefab = default;
    [SerializeField] private int m_MissileAmount = 0;
    public int MissileAmount { get => m_MissileAmount; }

    private float _nextAttackTime;
    private const float _attackDelay = 2f;
    
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
        _nextAttackTime = Time.time;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Fire();
        }
    }

    void Fire()
    {
        if (_nextAttackTime > Time.time) return;
        if (m_MissileAmount <= 0) return;
        
        m_MissileAmount -= 1;
        _nextAttackTime = Time.time + _attackDelay;
        
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = Vector3.Normalize(mousePosition - transform.position);

        Vector3 spawnPosition = transform.position + (direction * 1f);
        ProjectileMovement projectileCreated = Instantiate(_missilePrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtPosition(mousePosition);
    }

    public void AddMissile(int amount = 1)
    {
        m_MissileAmount += 1;
    }

}
