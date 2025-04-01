using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject.SpaceFighter;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[10, 10];

    public float coins;

    public TextMeshProUGUI coinsTxt;

    public GameObject[] itemPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinsTxt.text = "Coins: " + coins.ToString();

        //itemID
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        shopItems[1, 6] = 6;
        shopItems[1, 7] = 7;
        shopItems[1, 8] = 8;

        //price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 15;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 15;
        shopItems[2, 5] = 30;
        shopItems[2, 6] = 30;
        shopItems[2, 7] = 35;
        shopItems[2, 8] = 40;

        //quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
        shopItems[3, 6] = 0;
        shopItems[3, 7] = 0;
        shopItems[3, 8] = 0;
    }

    public void BuyItem()
    {
        Debug.Log("BuyItem called");

        GameObject buttonRef = EventSystem.current.currentSelectedGameObject;

        if (buttonRef != null)
        {
            ButtonInfo buttonInfo = buttonRef.GetComponent<ButtonInfo>();

            if (coins >= shopItems[2, buttonInfo.itemID])
            {
                coins -= shopItems[2, buttonInfo.itemID];
                shopItems[3, buttonInfo.itemID]++;

                coinsTxt.text = "Coins: " + coins.ToString();
                buttonInfo.quantityTxt.text = shopItems[3, buttonInfo.itemID].ToString();

                InstantiateItem(buttonInfo.itemID);
            }
        }
    }

    private void InstantiateItem(int itemID)
    {
        Debug.Log("Trying to instantiate item " + itemID);

        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            if (itemID > 0 && itemID <= itemPrefabs.Length)
            {
                GameObject itemPrefab = itemPrefabs[itemID - 1]; 

                Vector3 playerPosition = player.transform.position;
                Instantiate(itemPrefab, playerPosition, Quaternion.identity); 
            }
        }
        else
        {
            Debug.LogWarning("Player not found");
        }
    }
}
