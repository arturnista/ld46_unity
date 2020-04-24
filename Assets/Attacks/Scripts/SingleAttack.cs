using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Attacks/Single")]
public class SingleAttack : BaseAttack
{
    
    public override void Fire(Vector3 position, Vector3 attackPosition, GameObject prefab)
    {
        Vector3 direction = Vector3.Normalize(attackPosition - position);

        Vector3 spawnPosition = position + (direction * 1f);
        ProjectileMovement projectileCreated = Instantiate(prefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtDirection(direction);

    }

}
