using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnTable : ScriptableObject
{   

    [SerializeField]
    private List<EnemySpawnTableItem> m_Enemies;
    public List<EnemySpawnTableItem> Enemies { get { return m_Enemies; } set { m_Enemies = value; } }

	/// <summary>Get an random enemy prefab.</summary>
	/// <returns>Returns the enemy that will be spawned.</returns>
    public GameObject GetEnemyPrefab(float charge)
    {
        IEnumerable<EnemySpawnTableItem> enemiesAvailable = from enemy in Enemies
                                    where enemy.Charge <= charge
                                    select enemy;

        int enemiesAmountSum = 0;
        foreach (var enemy in enemiesAvailable)
        {
            enemiesAmountSum += enemy.Amout;
        }

        int dropAmount = Random.Range(0, enemiesAmountSum);
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