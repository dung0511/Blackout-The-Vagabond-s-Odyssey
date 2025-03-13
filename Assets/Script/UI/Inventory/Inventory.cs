using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.UI.Inventory
{
    public class Inventory
    {
        private List<Item> itemList;

        public Inventory()
        {
            itemList = new List<Item>();
            addItem(new Item { itemType = Item.ItemType.Weapon, amount = 1 });

            UnityEngine.Debug.Log(itemList.Count);
        }

        public void addItem(Item item)
        {
            itemList.Add(item);
        }
    }
}
