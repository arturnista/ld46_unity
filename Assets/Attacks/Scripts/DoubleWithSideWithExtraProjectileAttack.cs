using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Attacks/DoubleWithSideWithExtraProjectileAttack")]
public class DoubleWithSideWithExtraProjectileAttack : DoubleWithSideAttack
{
    
    [SerializeField] private GameObject _extraPrefab;

    public override void Fire(Vector3 position, Vector3 attackPosition, GameObject prefab)
    {
        base.Fire(position, attackPosition, prefab);
        
        // SHoots from behind, so direction is inverted
        Vector3 direction = -Vector3.Normalize(attackPosition - position);

        Vector3 spawnPosition = position + (direction * 1f);
        ProjectileMovement projectileCreated = Instantiate(_extraPrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtDirection(direction);
    }
}
