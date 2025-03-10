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
    public enum ItemType
    {
        Weapon,
        Potion,
        Default
    }

    public abstract class ItemObject : ScriptableObject
    {
        public GameObject prefab;
        public ItemType type;
        [TextArea(15, 20)]
        public string description;
    }

    
}