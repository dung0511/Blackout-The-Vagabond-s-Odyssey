using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Script.Service.IService
{
    public interface IPickService
    {
        GameObject GetPickGameOject();
        //void SetCharacterSRAndInHand();      
        //void UnSetCharacterSRAndInHand();
        void Pick();
        void Drop();
        bool IsPickingItemOrWeapon();
    }
}
