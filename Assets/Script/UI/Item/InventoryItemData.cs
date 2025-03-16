using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.UI.Inventory
{
    [CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
    public class InventoryItemData : ScriptableObject
    {
        public int id;
        public string displayName;
        [TextArea(4, 4)]
        public string description;
        public Sprite icon;
        public int maxStackSize;

    }
}
