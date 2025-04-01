using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;

    public TextMeshProUGUI priceTxt;

    public TextMeshProUGUI quantityTxt;

    public GameObject shopManager;

    void Update()
    {
        priceTxt.text = "Price: $" 
            + shopManager.GetComponent<ShopManager>().shopItems[2, itemID].ToString();
        quantityTxt.text = shopManager.GetComponent<ShopManager>().shopItems[3, itemID].ToString();
    }
}
