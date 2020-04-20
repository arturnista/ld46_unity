using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    
    [SerializeField] private DropTable _dropTable;

    private EnemyHealth _health;

    void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _health.OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        GameObject drop = _dropTable.GetDropPrefab();
        if (drop != null)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }

}
