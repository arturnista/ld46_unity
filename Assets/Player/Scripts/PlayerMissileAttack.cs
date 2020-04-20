using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileAttack : MonoBehaviour
{
    
    [SerializeField] private GameObject _missilePrefab = default;
    [SerializeField] private int m_MissileAmount = 0;
    public int MissileAmount { get => m_MissileAmount; }
    
    private Camera _camera;

    void Awake()
    {
        _camera = Camera.main;
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
        if (m_MissileAmount <= 0) return;
        m_MissileAmount -= 1;
        
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
