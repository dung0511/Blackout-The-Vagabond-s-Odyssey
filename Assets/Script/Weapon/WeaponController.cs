using Assets.Script.Service.IService;
using Assets.Script.Weapon.Throwable_Weapon;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject Weapon1;
    public GameObject Weapon2;
    public List<GameObject> ListObj;
    public bool haveOneWepon;

    private static GameObject sceneHolder;
    void Start()
    {
        ListObj = GetAllChild();
        foreach (GameObject obj in GetAllChild())
        {
            obj.SetActive(false);
        }
        if (ListObj.Count == 1)
        {
            Weapon1 = ListObj[0];
            haveOneWepon = true;
            Weapon1.SetActive(true);
        }
        else
        {
            Weapon1 = ListObj[0];
            Weapon2 = ListObj[1];
            haveOneWepon = false;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }

    }

    public void ChangeWeapon(int numPress)
    {
        switch (numPress)
        {
            case 1:
                if (haveOneWepon)
                {
                    Weapon1.SetActive(true);
                }
                else
                {
                    Weapon1.SetActive(true);
                    Weapon2.SetActive(false);
                }
                break;

            case 2:
                Weapon1.SetActive(false);
                Weapon2.SetActive(true);
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

    public void ChangeAndAddWeaponIfHaveOne(GameObject obj)
    {
        if (haveOneWepon)
        {
            AddWeapon(obj);
        }
        else
        {
            ChangeWeapon(obj);
        }

    }

    private void AddWeapon(GameObject newWeapon)
    {
        ListObj.Add(newWeapon);
        Weapon1.SetActive(false);
        Weapon2 = newWeapon;
        Weapon2.SetActive(true);
        haveOneWepon = false;
    }

    private void ChangeWeapon(GameObject newWeapon)
    {
        if (sceneHolder == null)
        {
            sceneHolder = new GameObject("SceneObjectHolder");
        }
        if (Weapon1.activeInHierarchy)
        {
            Weapon1.transform.SetParent(sceneHolder.transform, true);
            if (Weapon1.TryGetComponent<IPickService>(out var weapon))
            {
                weapon.Drop();
            }
            Weapon1 = newWeapon;
            ListObj[0] = newWeapon;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }
        else if (Weapon2.activeInHierarchy)
        {
            Weapon2.transform.SetParent(sceneHolder.transform, true);
            if (Weapon2.TryGetComponent<IPickService>(out var weapon))
            {
                weapon.Drop();
            }
            Weapon2 = newWeapon;
            ListObj[1] = newWeapon;
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }
    }
}
