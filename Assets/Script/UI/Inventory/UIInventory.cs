using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance { get; private set; }

    [SerializeField]
    private UIInventoryItem itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private UIInventoryDescription itemDescription;

    [SerializeField]
    private MouseFollower mouseFw;

    List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

    public event Action<int> OnDescriptionRequested,
        OnItemActionRequested,
        OnStartDragging;

    public event Action<int, int> OnSwapItems;

    private int currentlyDraggedItemIndex = -1;

    [SerializeField]
    private ItemActionPanel actionPanel;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }

        Instance = this; 
        DontDestroyOnLoad(gameObject);

        Hide();
        itemDescription.ResetDescription();
        mouseFw.Toggle(false);
    }

    public void InitializeInventoryUI(int size)
    {
        for (int i = 0; i < size; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);

            uiItem.transform.SetParent(contentPanel, false);

            RectTransform rectTransform = uiItem.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = new Vector2(100, 100);
            rectTransform.localScale = Vector3.one;

            listOfItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClick += HandleShowItemsActions;
        }
    }

    public void UpdateData(int itemIndex, Sprite itemeImage, int itemQuantity)
    {
        if (listOfItems.Count > itemIndex)
        {
            listOfItems[itemIndex].SetData(itemeImage, itemQuantity);
        }
    }

    private void HandleShowItemsActions(UIInventoryItem inventoryItemUI)
    {
        int index = listOfItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }
        OnItemActionRequested?.Invoke(index);
    }

    private void HandleEndDrag(UIInventoryItem inventoryItemUI)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UIInventoryItem inventoryItemUI)
    {
        int index = listOfItems.IndexOf(inventoryItemUI);
        if (index == -1)
        {
            return;
        }

        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
        HandleItemSelection(inventoryItemUI);
    }

    private void ResetDraggedItem()
    {
        mouseFw.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UIInventoryItem inventoryItemUI)
    {
        int index = listOfItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        currentlyDraggedItemIndex = index;

        HandleItemSelection(inventoryItemUI);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        mouseFw.Toggle(true);
        mouseFw.SetData(sprite, quantity);
    }

    public void HandleItemSelection(UIInventoryItem inventoryItemUI)
    {
        int index = listOfItems.IndexOf(inventoryItemUI);
        if (index == -1)
            return;
        OnDescriptionRequested?.Invoke(index);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ResetSelection();
    }

    public void ResetSelection()
    {
        itemDescription.ResetDescription();
        DeselectAllItems();
    }

    public void AddAction(string actionName, Action performAction)
    {
        actionPanel.AddButton(actionName, performAction); 
    }

    public void ShowItemAction(int itemIndex)
    {
        actionPanel.Toggle(true);
        actionPanel.transform.position = listOfItems[itemIndex].transform.position; 
    }

    private void DeselectAllItems()
    {
        foreach (UIInventoryItem item in listOfItems)
        {
            item.DeSelect();
        }
        actionPanel.Toggle(false);
    }

    public void Hide()
    {
        actionPanel.Toggle(false);
        gameObject.SetActive(false);
        ResetDraggedItem();
    }

    public void UpdateDescription(int itemIndex, Sprite itemImage, string name, string description)
    {
        itemDescription.SetDiscription(itemImage, name, description);
        DeselectAllItems();
        listOfItems[itemIndex].Select();
    }

    public void ResetAllItems()
    {
        foreach (UIInventoryItem item in listOfItems)
        {
            item.ResetData();
            item.DeSelect();
        }
    }
}
