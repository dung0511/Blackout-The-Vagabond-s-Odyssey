using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.UI.Inventory
{
    public class Item
    {
        public enum ItemType
        {
            Weapon,
            HealthPotion,
            Torch
        }

        public ItemType itemType;
        public int amount;
    }
}
