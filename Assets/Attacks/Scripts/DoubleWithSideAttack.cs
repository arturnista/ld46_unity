using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Attacks/DoubleWithSideAttack")]
public class DoubleWithSideAttack : DoubleAttack
{
    
    public override void Fire(Vector3 position, Vector3 attackPosition, GameObject prefab)
    {
        base.Fire(position, attackPosition, prefab);

        Vector3 direction = Vector3.Normalize(attackPosition - position);
        // To compute the right vector from the direction, we need the orthogonal angle between the direction and (0, 0, 1)
        Vector3 right = Vector3.Cross(Vector3.forward, direction);

        ProjectileMovement projectileCreated = null;
        Vector3 betweenRightDirection = (direction + right).normalized;
        Vector3 betweenLeftDirection = (direction - right).normalized;
        Vector3 rSpawnPosition = position + (betweenRightDirection * .1f);
        Vector3 lSpawnPosition = position + (betweenLeftDirection * .1f);
        projectileCreated = Instantiate(prefab, rSpawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtDirection(betweenRightDirection);

        projectileCreated = Instantiate(prefab, lSpawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtDirection(betweenLeftDirection);

        Debug.DrawRay(position, direction * 10f, Color.green, 1f);
        Debug.DrawRay(position, right * 10f, Color.magenta, 1f);

    }
}
