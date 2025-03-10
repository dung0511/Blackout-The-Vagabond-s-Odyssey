using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.UI.Inventory
{
    [CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Default")]

    public class DefaultObject : ItemObject
    {
        public void Awake()
        {
            type = ItemType.Default;
        }
    }
}
