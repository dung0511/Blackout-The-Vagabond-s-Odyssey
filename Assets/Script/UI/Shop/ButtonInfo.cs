using TMPro;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    public int itemID;

    public TextMeshProUGUI priceTxt;

    public TextMeshProUGUI quantityTxt;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        //hien thi gia va so luong da mua
        priceTxt.text = "Price: $" + ShopManager.Instance.GetPrice(itemID);
        quantityTxt.text = ShopManager.Instance.GetQuantity(itemID).ToString();
    }
}