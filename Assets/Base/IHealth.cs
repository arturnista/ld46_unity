using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHealth
{

    float MaxHealth { get; }
    float Health { get; }

    void DealDamage(float damage);

}
