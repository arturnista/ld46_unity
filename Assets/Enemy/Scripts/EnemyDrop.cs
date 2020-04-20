using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    
    [SerializeField] private GameObject _dropPrefab;
    [SerializeField] [Range(0, 100)] private int _dropChance;

    private EnemyHealth _health;

    void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _health.OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        int chance = Random.Range(0, 100);
        if (chance < _dropChance)
        {
            Instantiate(_dropPrefab, transform.position, Quaternion.identity);
        }
    }

}
