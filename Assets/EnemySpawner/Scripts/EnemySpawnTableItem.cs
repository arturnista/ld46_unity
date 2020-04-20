using UnityEngine;

[System.Serializable]
public struct EnemySpawnTableItem
{
    [Range(0, 30)]
    public int Amout;    
    [Range(0f, 100f)]
    public float Charge;    
    public GameObject Prefab;    

}