using Assets.Script.UI.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.UI.Item
{
    [RequireComponent(typeof(BoxCollider))]
    public class ItemPickUp : MonoBehaviour
    {
        public InventoryItemData itemData;

        private BoxCollider myCollider;

        private void Awake()
        {
            myCollider = GetComponent<BoxCollider>();
            myCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            var inventory = other.transform.GetComponent<InventoryHolder>();

            if (!inventory) return;

            if (inventory.InventorySystem.addToInventory(itemData, 1))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
