using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Scriptable Objects/Loot/Table")]
public class LootTableSO : ScriptableObject
{
    public List<LootTable> table;
}
