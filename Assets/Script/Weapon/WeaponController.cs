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
            Weapon1.active = true;
        }
        else
        {
            Weapon1 = ListObj[0];
            Weapon2 = ListObj[1];
            haveOneWepon = false;
            Weapon1.active = true;
            Weapon2.active = false;
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
            bool isPickingRanged = obj.GetComponent<RangedWeapon>() != null;
            bool isPickingMelee = obj.GetComponent<MeleeWeapon>() != null;
            if ((isHoldingMelee && isPickingRanged) || (isHoldingRanged && isPickingMelee))
            {

                ReplaceWeapon(obj);
            }
            else
            {

                SwapWeapon(obj);
            }
            //    if (GetComponentInChildren<RangedWeapon>() != null)
            //{
            //    if (Weapon1.activeInHierarchy)
            //    {
            //        //Instantiate(Weapon1, playerPos, Quaternion.identity);
            //        Weapon1.transform.SetParent(null, true);
            //        Weapon1.GetComponent<RangedWeapon>().InGround(Weapon1);
            //        Weapon1 = obj;
            //        Weapon1.SetActive(true);
            //        Weapon2.SetActive(false);

            //    }
            //    else if (Weapon2.activeInHierarchy)
            //    {
            //        Weapon2.transform.SetParent(null, true);
            //        Weapon2.GetComponent<RangedWeapon>().InGround(Weapon2);
            //        Weapon2 = obj;
            //        Weapon1.SetActive(false);
            //        Weapon2.SetActive(true);
            //    }
            //}
            //else if (GetComponentInChildren<MeleeWeapon>() != null)
            //{
            //    if (Weapon1.activeInHierarchy)
            //    {
            //        //Instantiate(Weapon1, playerPos, Quaternion.identity);
            //        Weapon1.transform.SetParent(null, true);
            //        Weapon1.GetComponent<MeleeWeapon>().InGround(Weapon1);
            //        Weapon1 = obj;
            //        Weapon1.SetActive(true);
            //        Weapon2.SetActive(false);

            //    }
            //    else if (Weapon2.activeInHierarchy)
            //    {
            //        Weapon2.transform.SetParent(null, true);
            //        Weapon2.GetComponent<MeleeWeapon>().InGround(Weapon2);
            //        Weapon2 = obj;
            //        Weapon1.SetActive(false);
            //        Weapon2.SetActive(true);
            //    }
            //}
        }

    }

    private void SwapWeapon(GameObject newWeapon)
    {
        if (Weapon1.activeInHierarchy)
        {
            Weapon1.transform.SetParent(null, true);

            if (Weapon1.GetComponent<RangedWeapon>() != null)
                Weapon1.GetComponent<RangedWeapon>().InGround(Weapon1);
            else if (Weapon1.GetComponent<MeleeWeapon>() != null)
                Weapon1.GetComponent<MeleeWeapon>().InGround(Weapon1);

            Weapon1 = newWeapon;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }
        else if (Weapon2.activeInHierarchy)
        {
            Weapon2.transform.SetParent(null, true);

            if (Weapon2.GetComponent<RangedWeapon>() != null)
                Weapon2.GetComponent<RangedWeapon>().InGround(Weapon2);
            else if (Weapon2.GetComponent<MeleeWeapon>() != null)
                Weapon2.GetComponent<MeleeWeapon>().InGround(Weapon2);

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
                Weapon1.GetComponent<RangedWeapon>().InGround(Weapon1);
            else if (Weapon1.GetComponent<MeleeWeapon>() != null)
                Weapon1.GetComponent<MeleeWeapon>().InGround(Weapon1);

            Weapon1 = newWeapon;
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
        }
        else if (Weapon2.activeInHierarchy)
        {
            Weapon2.transform.SetParent(null, true);

            if (Weapon2.GetComponent<RangedWeapon>() != null)
                Weapon2.GetComponent<RangedWeapon>().InGround(Weapon2);
            else if (Weapon2.GetComponent<MeleeWeapon>() != null)
                Weapon2.GetComponent<MeleeWeapon>().InGround(Weapon2);

            Weapon2 = newWeapon;
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
        }


    }


}
