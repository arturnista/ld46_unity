using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Attacks/Double")]
public class DoubleAttack : BaseAttack
{
    
    public override void Fire(Vector3 position, Vector3 attackPosition, GameObject prefab)
    {
        Vector3 direction = Vector3.Normalize(attackPosition - position);
        // To compute the right vector from the direction, we need the orthogonal angle between the direction and (0, 0, 1)
        Vector3 right = Vector3.Cross(Vector3.forward, direction);

        Vector3 fSpawnPosition = position + (direction * 1f) + (right * .15f);
        Vector3 sSpawnPosition = position + (direction * 1f) - (right * .15f);
        ProjectileMovement projectileCreated = null;

        projectileCreated = Instantiate(prefab, fSpawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtDirection(direction);

        projectileCreated = Instantiate(prefab, sSpawnPosition, Quaternion.identity).GetComponent<ProjectileMovement>();
        projectileCreated.ShootAtDirection(direction);

        Debug.DrawRay(position, direction * 10f, Color.green, 1f);
        Debug.DrawRay(position, right * 10f, Color.magenta, 1f);

    }
}
