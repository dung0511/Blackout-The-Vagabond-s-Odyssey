using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;
    public Image icon;
    public TextMeshProUGUI priceTxt;
    public TextMeshProUGUI quantityTxt;

    [HideInInspector] public GameObject itemPrefab;

    public void SetupSlot(GameObject prefab, int id, int price)
    {
        itemPrefab = prefab;
        itemID = id;

        if (icon != null)
        {
            var spriteRenderer = prefab.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                icon.sprite = spriteRenderer.sprite;
            }
            else
            {
                var itemScript = prefab.GetComponent<Item>();
                if (itemScript != null && itemScript.inventoryItem != null)
                {
                    icon.sprite = itemScript.inventoryItem.ItemImage;
                }
            }
        }

        priceTxt.text = " " + price;
        quantityTxt.text = ShopManager.Instance.GetQuantity(itemID).ToString();
    }

    public void UpdateUI()
    {
        int quantity = ShopManager.Instance.GetQuantity(itemID);
        quantityTxt.text = quantity.ToString();

        bool isAvailable = quantity > 0;
        GetComponent<Button>().interactable = isAvailable;

        if (icon != null)
        {
            icon.color = isAvailable ? Color.white : new Color(1f, 1f, 1f, 0.3f); 
        }
    }



    public void Buy()
    {
        ShopManager.Instance.BuyItem(itemID);
    }
}
