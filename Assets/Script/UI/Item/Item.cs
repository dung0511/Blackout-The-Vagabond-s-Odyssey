using Assets.Script.Service.IService;
using System;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour, IPickService
{
    [field: SerializeField]
    public ItemSO inventoryItem { get; private set; }

    [field: SerializeField]
    public int quantity { get; set; } = 1;

    //[SerializeField]
    //private AudioSource audioSource;

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
        //pick logic
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
