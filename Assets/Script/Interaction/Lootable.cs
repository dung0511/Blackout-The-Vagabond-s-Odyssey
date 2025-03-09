using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lootable : MonoBehaviour
{
    [SerializeField] private LootTableSO lootTables;
    public void DropLoot()
    {
        foreach (var table in lootTables.table)
        {
            if (Utility.Chance(table.tableChance))
            {
                GameObject loot = table.loots[Utility.UnseededRng(0, table.loots.Count)];
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
    }
}

public class LootTable 
{
    public List<GameObject> loots = new List<GameObject>();
    public int tableChance = 50;
}


[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/Loot/Table")]
public class LootTableSO : ScriptableObject
{
    public List<LootTable> table;
}