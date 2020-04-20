using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : MonoBehaviour
{

    [SerializeField] private float _healAmount;

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerHealth player = collider.GetComponent<PlayerHealth>();
        if (player)
        {
            player.Heal(_healAmount);
            Destroy(gameObject);
        }
    }

}
