using System;
using System.Collections.Generic;
using UnityEngine;

public interface ILootable 
{
    public void DropLoot();
}

[Serializable]
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