using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DropTable : ScriptableObject
{   
    [SerializeField]
    [Range(0, 50)]
    private int m_NotDropAmount;
    public int NotDropAmount { get { return m_NotDropAmount; } set { m_NotDropAmount = value; } }
    [SerializeField]
    private List<DropTableItem> m_Items;
    public List<DropTableItem> Items { get { return m_Items; } set { m_Items = value; } }

    private int m_ItemsAmountSum;

    void OnEnable()
    {
        m_ItemsAmountSum = m_NotDropAmount;
        foreach (DropTableItem item in Items)
        {
            m_ItemsAmountSum += item.Amout;
        }
    }

	/// <summary>Get an item from the drop table.</summary>
	/// <returns>Returns the item prefab OR null if none is dropped.</returns>
    public GameObject GetDropPrefab()
    {
        int dropAmount = Random.Range(0, m_ItemsAmountSum);
        foreach (DropTableItem item in Items)
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