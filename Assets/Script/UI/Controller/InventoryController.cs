using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour, IUIScreen
{
    [SerializeField]
    private UIInventory inventoryUI;

    [SerializeField]
    public InventorySO inventoryData;

    private PlayerHealthController playerHealth;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
        playerHealth = GetComponent<PlayerHealthController>();
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryChanged += UpdateInventoryUI;
        foreach (InventoryItem item in initialItems)
        {
            if (item.isEmpty)
                continue;
            inventoryData.AddItem(item);

        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage,
                item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.size);
        this.inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty) return;

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject);
            if (inventoryData.GetItemAt(itemIndex).isEmpty)
            {
                inventoryUI.ResetSelection();
            }
        }
    }

    private void HandleItemActionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty) return;

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            inventoryUI.ShowItemAction(itemIndex);
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
        }

        IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
        if (destroyableItem != null)
        {
            inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
        }
    }

    private void DropItem(int itemIndex, int quantity)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty || inventoryItem.item.DropPrefab == null) return;

        int quantityToDrop = inventoryItem.quantity;

        Vector3 dropPosition = transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), 0.5f, 0);

        GameObject droppedItemObject = Instantiate(inventoryItem.item.DropPrefab, dropPosition, Quaternion.identity);
        Item droppedItem = droppedItemObject.GetComponent<Item>();
        if (droppedItem != null)
        {
            droppedItem.inventoryItem = inventoryItem.item;
            droppedItem.quantity = quantityToDrop;
        }

        inventoryData.RemoveItem(itemIndex, quantityToDrop);
        inventoryUI.ResetSelection();
    }


    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty)
            return;
        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex1, int itemIndex2)
    {
        inventoryData.SwapItems(itemIndex1, itemIndex2);
    }

    private void HandleDescriptionRequest(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.isEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }

        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage,
            item.name, item.Description);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UIManager.Instance.ToggleScreen(this);
        }
    }

    public void Open()
    {
        inventoryUI.Show();
        foreach (var item in inventoryData.GetCurrentState())
        {
            inventoryUI.UpdateData(item.Key,
                item.Value.item.ItemImage,
                item.Value.quantity);
        }
    }

    public void Close()
    {
        inventoryUI.Hide();
    }
}
