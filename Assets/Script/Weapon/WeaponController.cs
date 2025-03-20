using Assets.Script.Weapon.Throwable_Weapon;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public int currentWeaponIndex = 0;

    public GameObject Weapon1;
    public GameObject Weapon2;
    public List<GameObject> ListObj;
    public bool haveOneWepon;
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
                currentWeaponIndex = 0;
                break;

            case 2:
                Weapon1.SetActive(false);
                Weapon2.SetActive(true);
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

    public void ChangeAndAddWeaponIfHaveOne(GameObject obj)
    {
        if (haveOneWepon)
        {
            ListObj.Add(obj);
            Weapon1.SetActive(false);
            Weapon2 = obj;
            Weapon2.SetActive(true);
            haveOneWepon = false;
        }
        else
        {
            bool isHoldingRanged = GetComponentInChildren<RangedWeapon>() != null;
            bool isHoldingMelee = GetComponentInChildren<MeleeWeapon>() != null;
            bool isHoldingThrowable = GetComponentInChildren<ThrowableWeapon>() != null;
            bool isPickingRanged = obj.GetComponent<RangedWeapon>() != null;
            bool isPickingMelee = obj.GetComponent<MeleeWeapon>() != null;
            bool isPickingThrowable = obj.GetComponent<ThrowableWeapon>() != null;
            if ((isHoldingMelee && (isPickingRanged || isPickingThrowable)) ||
           (isHoldingRanged && (isPickingMelee || isPickingThrowable)) ||
           (isHoldingThrowable && (isPickingMelee || isPickingRanged)))
            {

                ReplaceWeapon(obj);
            }
            else if ((isHoldingThrowable && isPickingThrowable) || (isHoldingMelee&&isPickingMelee) || (isHoldingRanged&& isPickingRanged))
            {

                SwapWeapon(obj);
            }

        }

    }

    private void SwapWeapon(GameObject newWeapon)
    {
        if (Weapon1.activeInHierarchy)
        {
            Weapon1.transform.SetParent(null, true);

            if (Weapon1.GetComponent<RangedWeapon>() != null)
            {
                //Weapon1.GetComponent<RangedWeapon>().InGround(Weapon1);
            }
            else if (Weapon1.GetComponent<MeleeWeapon>() != null)
            {
                Weapon1.GetComponent<MeleeWeapon>().meleeWeapon.DropWeapon(Weapon1);
            }
            else if (Weapon1.GetComponent<ThrowableWeapon>() != null)
            {
                Weapon1.GetComponent<ThrowableWeapon>().throwStraightWeapon.DropWeapon(Weapon1);
            }

            Weapon1 = newWeapon;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }
        else if (Weapon2.activeInHierarchy)
        {
            Weapon2.transform.SetParent(null, true);

            if (Weapon2.GetComponent<RangedWeapon>() != null)
            {
               // Weapon2.GetComponent<RangedWeapon>().InGround(Weapon2);
            }

            else if (Weapon2.GetComponent<MeleeWeapon>() != null)
            {
                Weapon2.GetComponent<MeleeWeapon>().meleeWeapon.DropWeapon(Weapon2);
            }

            else if (Weapon2.GetComponent<ThrowableWeapon>() != null)
            {
                Weapon2.GetComponent<ThrowableWeapon>().throwStraightWeapon.DropWeapon(Weapon2);
            }
            Weapon2 = newWeapon;
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }
    }


    private void ReplaceWeapon(GameObject newWeapon)
    {
        if (Weapon1.activeInHierarchy)
        {
            Weapon1.transform.SetParent(null, true);

            if (Weapon1.GetComponent<RangedWeapon>() != null)
            {
               // Weapon1.GetComponent<RangedWeapon>().InGround(Weapon1);
            }

            else if (Weapon1.GetComponent<MeleeWeapon>() != null)
            {
                Weapon1.GetComponent<MeleeWeapon>().meleeWeapon.DropWeapon(Weapon1);
            }

            else if (Weapon1.GetComponent<ThrowableWeapon>() != null)
            {
                Weapon1.GetComponent<ThrowableWeapon>().throwStraightWeapon.DropWeapon(Weapon1);
            }

            Weapon1 = newWeapon;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }
        else if (Weapon2.activeInHierarchy)
        {
            Weapon2.transform.SetParent(null, true);

            if (Weapon2.GetComponent<RangedWeapon>() != null)
            {
               // Weapon2.GetComponent<RangedWeapon>().InGround(Weapon2);
            }

            else if (Weapon2.GetComponent<MeleeWeapon>() != null)
            {
                Weapon2.GetComponent<MeleeWeapon>().meleeWeapon.DropWeapon(Weapon2);
            }

            else if (Weapon2.GetComponent<ThrowableWeapon>() != null)
            {
                Weapon2.GetComponent<ThrowableWeapon>().throwStraightWeapon.DropWeapon(Weapon2);
            }
            Weapon2 = newWeapon;
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }


    }


}
