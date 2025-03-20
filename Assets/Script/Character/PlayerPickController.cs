using Assets.Script.Weapon.Throwable_Weapon;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
//using static UnityEngine.GraphicsBuffer;

public class PlayerPickController : MonoBehaviour
{
    public bool isTouchItem = false;
    public bool isMelee = false;
    public bool isRanged = false;
    public bool isThrowable = false;
    public GameObject Item;

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.GetComponent<MeleeWeapon>() != null)
        {
            isTouchItem = true;
            Item = collision.gameObject;
            isMelee = true;
            isRanged = false;
            isThrowable = false;
        }
        if (collision.GetComponent<RangedWeapon>() != null)
        {
            isTouchItem = true;
            Item = collision.gameObject;
            isMelee = false;
            isRanged = true;
            isThrowable = false;
        }
        if (collision.GetComponent<ThrowableWeapon>() != null)
        {
            isTouchItem = true;
            Item = collision.gameObject;
            isMelee = false;
            isRanged = false;
            isThrowable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.GetComponent<MeleeWeapon>() != null)
        {
            isTouchItem = true;
            Item = collision.gameObject;
            isMelee = true;
            isRanged = false;
            isThrowable = false;
        }
        if (collision.GetComponent<RangedWeapon>() != null)
        {
            isTouchItem = true;
            Item = collision.gameObject;
            isMelee = false;
            isRanged = true;
            isThrowable = false;
        }
        if (collision.GetComponent<ThrowableWeapon>() != null)
        {
            isTouchItem = true;
            Item = collision.gameObject;
            isMelee = false;
            isRanged = false;
            isThrowable = true;
        }
    }
    private void OnTriggerExit2D()
    {
        isMelee = false;
        isRanged = false;
        isThrowable = false;
        isTouchItem = false;
        Item = null;
    }



    public void PickItemWeapon()
    {
        if (isMelee)
        {
            //Item.transform.SetParent(GetComponentInChildren<WeaponController>().gameObject.transform, false);

            //Item.GetComponent<MeleeWeapon>().meleeWeapon._characterSR = GetComponentInChildren<SpriteRenderer>();
            //Item.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            //Item.transform.localPosition = new Vector3(0, -0.03f, 0);

            //Item.GetComponentInChildren<MeleeWeapon>().inHand = true;
            //GetComponentInChildren<WeaponController>().ChangeAndAddWeaponIfHaveOne(Item);
        }
        else if (isRanged)
        {
            //Item.transform.SetParent(GetComponentInChildren<WeaponController>().gameObject.transform, false);

            //Item.GetComponent<RangedWeapon>().currentCharacterSR = GetComponentInChildren<SpriteRenderer>();
            //Item.transform.localScale = new Vector3(0.6f, 0.6f, 0);
            //Item.transform.localPosition = new Vector3(0, -0.04f, 0);

            //Item.GetComponentInChildren<RangedWeapon>().inHand = true;
            //GetComponentInChildren<WeaponController>().ChangeAndAddWeaponIfHaveOne(Item);
        }
        else if (isThrowable)
        {
            //Item.transform.SetParent(GetComponentInChildren<WeaponController>().gameObject.transform, false);

            //Item.GetComponent<ThrowableWeapon>().currentCharacterSR = GetComponentInChildren<SpriteRenderer>();
            //Item.transform.localScale = new Vector3(0.7f, 0.7f, 0);
            //Item.transform.localPosition = new Vector3(0, -0.04f, 0);

            //Item.GetComponentInChildren<ThrowableWeapon>().inHand = true;
            //GetComponentInChildren<WeaponController>().ChangeAndAddWeaponIfHaveOne(Item);
        }

    }

}