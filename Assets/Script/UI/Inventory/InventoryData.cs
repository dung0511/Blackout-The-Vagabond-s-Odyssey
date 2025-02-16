using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.UI.Inventory
{
    [System.Serializable]
    public class InventoryData
    {
        public List<ItemData> items = new List<ItemData>();
    }

    [System.Serializable]
    public class ItemData
    {
        public string itemName;
        public int slotIndex;
    }


}
