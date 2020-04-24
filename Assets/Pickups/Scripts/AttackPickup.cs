using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPickup : MonoBehaviour
{
    
    [SerializeField] private List<BaseAttack> _attackOrder = null;

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerRangeAttack player = collider.GetComponent<PlayerRangeAttack>();
        if (player)
        {
            for (int i = 0; i < _attackOrder.Count - 1; i++)
            {
                if (_attackOrder[i] == player.Attack)
                {
                    player.Attack = _attackOrder[i + 1];
                    break;
                }
            }
            Destroy(gameObject);
        }
    }


}
