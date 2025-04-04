//using Assets.Script.Service.IService;
using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IPick
{
    [field: SerializeField]
    public ItemSO inventoryItem { get; set; }

    [field: SerializeField]
    public int quantity { get; set; } = 1;

    [SerializeField]
    private float duration = 0.3f;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = inventoryItem.ItemImage;
    }

    public void DestroyItem()
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickUp());
    }

    public IEnumerator AnimateItemPickUp()  
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duration);
            yield return null;
        }
        Destroy(gameObject);
    }

    public GameObject GetPickGameOject()
    {
       return gameObject;
    }

    public void Pick()
    {
        InventoryController inventory = FindObjectOfType<InventoryController>();
        if (inventory == null) return;

        int remaining = inventory.inventoryData.AddItem(this.inventoryItem, this.quantity);

        if (remaining <= 0)
        {
            DestroyItem();
        }
    }

    public void Drop()
    {
        //drop logic
    }
    //return false la item, return true la weapon
    public bool IsPickingItemOrWeapon()
    {
        return false;
    }
}
