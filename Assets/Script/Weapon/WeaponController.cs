using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    
    public int currentWeaponIndex=0;
    
    public GameObject Weapon1;
    public GameObject Weapon2;
    public List<GameObject> obj;
   // public RangedWeapon rangedWeapon;
   // public MeleeWeapon meleeWeapon;
    void Start()
    {
        obj = new List<GameObject>();
        obj = GetAllChild();
        foreach(GameObject obj in GetAllChild())
        {
            obj.SetActive(false);
        }
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
                currentWeaponIndex = 0;
                break;

            case 2:
                Weapon1.active = false;
                Weapon2.active = true;
                currentWeaponIndex = 1;
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
