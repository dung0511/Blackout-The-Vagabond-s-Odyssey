using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.UI.Inventory.Object
{
    [CreateAssetMenu(fileName = "New Potion Object", menuName = "Inventory System/Items/Potion")]

    public class PotionObject : ItemObject
    {
        public int regen;

        public void Awake()
        {
            type = ItemType.Potion;
        }
    }
}
