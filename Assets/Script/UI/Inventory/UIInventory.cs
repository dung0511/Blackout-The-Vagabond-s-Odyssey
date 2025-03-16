using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

    public void InitializeInventoryUI(int size)
    {
        for (int i = 0; i < size; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag  += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemsAction;
        }
    }

    private void HandleShowItemsAction(UIInventoryItem obj)
    {
        
    }

    private void HandleEndDrag(UIInventoryItem obj)
    {
        
    }

    private void HandleSwap(UIInventoryItem obj)
    {
        
    }

    private void HandleBeginDrag(UIInventoryItem obj)
    {
        
    }

    private void HandleItemSelection(UIInventoryItem obj)
    {
        Debug.Log(obj.name);    
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}   
