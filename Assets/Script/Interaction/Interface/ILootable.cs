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
    public List<GameObject> loots;
    public int tableChance = 50;
}


