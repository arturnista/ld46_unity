using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilePickup : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMissileAttack player = collider.GetComponent<PlayerMissileAttack>();
        if (player)
        {
            player.AddMissile();
            Destroy(gameObject);
        }
    }

}
