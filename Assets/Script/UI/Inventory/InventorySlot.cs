using Assets.Script.UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (transform.childCount == 0)
        {
            draggableItem.parentAfterDrag = transform;
        }
        else
        {
            Transform existingItem = transform.GetChild(0);
            existingItem.SetParent(draggableItem.parentAfterDrag);
            existingItem.localPosition = Vector3.zero;

            draggableItem.parentAfterDrag = transform;
        }
    }

    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;
    public InventorySlot (InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot ()
    {
        clearSlot();    
    }

    public void clearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void addToStack(int amount)
    {
        stackSize += amount;
    }

    public void removeFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool roomLeftInStack(int amountToAdd, out int amountRemaining)
    {
        amountRemaining = ItemData.maxStackSize - stackSize;

        return roomLeftInStack(amountToAdd);
    }

    public bool roomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= ItemData.maxStackSize)
        {
            return true;
        }

        else return false;
    }
}