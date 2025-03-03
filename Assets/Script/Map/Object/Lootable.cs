using System.Collections.Generic;
using UnityEngine;

public abstract class Lootable : MonoBehaviour
{
    [SerializeField] protected List<LootTable> lootTables;
    protected void DropLoot()
    {
        foreach (LootTable table in lootTables)
        {
            if (Utility.Chance(table.tableChance))
            {
                GameObject loot = table.loot[Utility.UnseededRng(0, table.loot.Count)];
                Instantiate(loot, transform.position, Quaternion.identity);
            }
        }
    }
}

public class LootTable
{
    public List<GameObject> loot;
    public int tableChance = 50;
}