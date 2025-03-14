using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Script.UI.Inventory
{
    [System.Serializable]
    public class InventorySystem
    {
        [SerializeField] private List<InventorySlot> inventorySlots;

        public List<InventorySlot> InventorySlots => inventorySlots;

        public int InventorySize => inventorySlots.Count;

        public UnityAction<InventorySlot> OnInventorySlotChanged;

        public InventorySystem(int size)
        {
            inventorySlots = new List<InventorySlot>(size);

            for (int i = 0; i < size; i++)
            {
                inventorySlots.Add(new InventorySlot());
            }
        }

        public bool addToInventory(InventoryItemData itemToAdd, int amountToAdd)
        {
            inventorySlots[0] = new InventorySlot(itemToAdd, amountToAdd);

            return true;
        }
    }
}
