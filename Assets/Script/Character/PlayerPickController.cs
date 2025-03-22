using Assets.Script.Service.IService;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IPickService>(out var weapon))
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

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.TryGetComponent<IPickService>(out var weapon))
        {
            Item = weapon.GetPickGameOject();
            isTouchItem = true;
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
            Item.transform.SetParent(GetComponentInChildren<WeaponController>().gameObject.transform, false);
            GetComponentInChildren<WeaponController>().ChangeAndAddWeaponIfHaveOne(Item);
            var weapon = Item.GetComponent<IPickService>();
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