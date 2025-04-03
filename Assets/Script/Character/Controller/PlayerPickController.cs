
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
    [SerializeField]
    private InventorySO inventoryData;
    public bool isTouchItem = false;
    public GameObject Item;
    public bool isWeapon;
    BaseWeapon weapon1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IPick>(out var weapon))
        {
            Item = weapon.GetPickGameOject();
            isTouchItem = true;
            // them UI hien thong tin vu khi la cai weapon detail so
            // 
            weapon1=Item.GetComponent<BaseWeapon>();
            weapon1.GetWeaponDetailSO(); 
            if (weapon.IsPickingItemOrWeapon())
            {
                isWeapon = true;
            }
            else
            {
                isWeapon = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<IPick>(out var weapon))
        {
            Item = weapon.GetPickGameOject();
            isTouchItem = true;
            if (weapon.IsPickingItemOrWeapon())
            {
                isWeapon = true;
            }
            else
            {
                isWeapon = false;
            }
        }

    }
    private void OnTriggerExit2D()
    {
        isTouchItem = false;
        Item = null;
    }

    public void PickItemWeapon()
    {
        if (isWeapon)
        {
            Item.transform.SetParent(GetComponentInChildren<PlayerWeaponController>().gameObject.transform, false);
            GetComponentInChildren<PlayerWeaponController>().ChangeAndAddWeaponIfHaveOne(Item);
            var weapon = Item.GetComponent<IPick>();
            weapon.Pick();
        }
        else
        {
            var item = Item.GetComponent<Item>();
            int reminder = inventoryData.AddItem(item.inventoryItem, item.quantity);
            if (reminder == 0)
            {
                item.DestroyItem();
            }
            else
            {
                item.quantity = reminder;
            }
        }

    }

}