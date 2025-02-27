using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public int currentWeaponIndex=1;
    
    public GameObject Weapon1;
    public GameObject Weapon2;
   // public RangedWeapon rangedWeapon;
   // public MeleeWeapon meleeWeapon;
    void Start()
    {
        Weapon1 = GetAllChild()[0];
        Weapon2 = GetAllChild()[1];
        Weapon1.active = true;
        Weapon2.active = false;
    }
    
    public void ChangeWeapon(int numPress)
    {
        switch (numPress)
        {
            case 1:
                Weapon1.active = true;
                Weapon2.active = false;
              
                break;

            case 2:
                Weapon1.active = false;
                Weapon2.active = true;
               
                break;
        }
    }
    List<GameObject> GetAllChild()
    {
        List<GameObject> list = new List<GameObject>();
        Transform parentTransform = this.transform; 
        foreach (Transform child in parentTransform)
        {
            list.Add(child.gameObject);
        }
        return list;
    }
}
