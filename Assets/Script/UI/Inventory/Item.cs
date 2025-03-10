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
    [CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public int value;

    }

    
}