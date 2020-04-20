using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeAttack : MonoBehaviour
{

    [SerializeField] private float _fireRate = 1f;
    [SerializeField] private GameObject _projectilePrefab = default;
    [Space]
    [SerializeField] private List<AudioClip> _attackSounds = default;

    private AudioSource _audioSource;
    private float _fireDelay;
    private float _fireTime;

    private Camera _camera;
    private bool _isFiring;

    void Awake()
    {
        _camera = Camera.main;

        _fireDelay = 1f / _fireRate;
        _fireTime = _fireDelay;

        _audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        _fireTime += Time.deltaTime;
        if (_isFiring && _fireTime >= _fireDelay)
        {
            _fireTime = 0f;
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleFireDown();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            HandleFireUp();
        }
    }

    void HandleFireDown()
    {
        _isFiring = true;
    }

    void HandleFireUp()
    {
        _isFiring = false;
    }

    void Fire()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector3 direction = Vector3.Normalize(mousePosition - transform.position);

        Vector3 spawnPosition = transform.position + (direction * 1f);
        ProjectileMovement projectileCreated = Instantiate(_projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtPosition(mousePosition);

        if (_attackSounds.Count > 0)
        {
            _audioSource.PlayOneShot(_attackSounds[Random.Range(0, _attackSounds.Count)]);
        }
    }

}
