using Assets.Script.UI.Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

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
}