using UnityEngine;

[System.Serializable]
public struct DropTableItem
{
    [Range(0, 30)]
    public int Amout;    
    public GameObject Prefab;    

}