using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Script.UI.Inventory
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Item")]

    public class Item : ScriptableObject
    {
        [Header("Only UI")]
        public bool stackable = true;

        [Header("Only Gameplay")]
        public ItemType type;

        // Common properties for all items
        public string itemName;
        public Sprite icon;
    }

    public enum ItemType
    {
        Equipment,
        Weapon,
        Potion
    }

    public enum ActionType
    {
        Attack,
        Equip,
        Use
    }

    // Subclass for Weapon
    [CreateAssetMenu(menuName = "Scriptable object/Weapon")]
    public class Weapon : Item
    {
        public int damage;
        public float attackRange;

        public Weapon()
        {
            type = ItemType.Weapon;
            stackable = false; // Weapons are usually not stackable
        }
    }

    // Subclass for Potion
    [CreateAssetMenu(menuName = "Scriptable object/Potion")]
    public class Potion : Item
    {
        public int regen;

        public Potion()
        {
            type = ItemType.Potion;
            stackable = true; // Potions are usually stackable
        }
    }
}