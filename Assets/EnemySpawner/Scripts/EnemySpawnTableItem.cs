using UnityEngine;

[System.Serializable]
public struct EnemySpawnTableItem
{
    [Range(0, 30)]
    public int Amout;    
    public GameObject Prefab;    

}