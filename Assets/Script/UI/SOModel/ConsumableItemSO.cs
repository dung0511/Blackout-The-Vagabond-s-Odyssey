using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ConsumableItemSO : ItemSO, IDestroyableItem, IItemAction
{
    [SerializeField]
    private List<ModifierData> modifiersData = new List<ModifierData>();

    public string ActionName => "Consume";

    public bool PerformAction(GameObject player)
    {
        foreach (ModifierData data in modifiersData)
        {
            data.statsModifier.AffectCharcater(player, data.value);
        }
        return true;
    }
}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }

    bool PerformAction(GameObject player);
}

[Serializable]
public class ModifierData
{
    public CharacterStatsModifierSO statsModifier;

    public float value;

}