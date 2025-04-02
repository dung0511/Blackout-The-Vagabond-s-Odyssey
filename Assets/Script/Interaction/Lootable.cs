using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Lootable : MonoBehaviour, ILootable
{
    [SerializeField] private LootTableSO lootTables;
    [SerializeField] private float dropRadius = 2;
    public void DropLoot()
    {
        foreach (var table in lootTables.table)
        {
            if (Utility.Chance(table.tableChance))
            {
                GameObject loot = table.loots[Utility.UnseededRng(0, table.loots.Count)];
                var randomPosition = Utility.GetRandomPositionInCircle(transform.position, dropRadius);
                Instantiate(loot, randomPosition, Quaternion.identity);
            }
        }
    }

}