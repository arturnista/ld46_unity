using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileAttack : MonoBehaviour
{
    
    [SerializeField] private GameObject _missilePrefab = default;
    [SerializeField] private int _missileAmount = 0;
    public int MissileAmount { get => _missileAmount; }
    
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
        if (_missileAmount <= 0) return;
        _missileAmount -= 1;
        
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = Vector3.Normalize(mousePosition - transform.position);

        Vector3 spawnPosition = transform.position + (direction * 1f);
        ProjectileMovement projectileCreated = Instantiate(_missilePrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtPosition(mousePosition);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "MissilePickup")
        {
            _missileAmount += 1;
            Destroy(collider.gameObject);
        }
    }

}
