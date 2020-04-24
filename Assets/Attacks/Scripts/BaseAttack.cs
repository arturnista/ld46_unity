using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttack : ScriptableObject
{

    public abstract void Fire(Vector3 position, Vector3 direction, GameObject prefab);

}
