using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.UI.Inventory.Object
{
    [CreateAssetMenu(fileName = "New Weapon Object", menuName = "Inventory System/Items/Weapon")]

    public class WeaponObject : ItemObject
    {
        public float damage;

        public void Awake()
        {
            type = ItemType.Weapon;
        }
    }
}
