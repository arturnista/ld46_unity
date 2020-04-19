using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnTable : ScriptableObject
{   

    [SerializeField]
    private List<EnemySpawnTableItem> m_Enemies;
    public List<EnemySpawnTableItem> Enemies { get { return m_Enemies; } set { m_Enemies = value; } }

    private int _enemiesAmountSum;

    void OnEnable()
    {
        _enemiesAmountSum = 0;
        foreach (EnemySpawnTableItem item in Enemies)
        {
            _enemiesAmountSum += item.Amout;
        }
    }

	/// <summary>Get an random enemy prefab.</summary>
	/// <returns>Returns the enemy that will be spawned.</returns>
    public GameObject GetEnemyPrefab()
    {
        int dropAmount = Random.Range(0, _enemiesAmountSum);
        foreach (EnemySpawnTableItem item in Enemies)
        {
            if(dropAmount <= item.Amout)
            {
                return item.Prefab;
            }

            dropAmount -= item.Amout;
        }

        return null;
    }

}