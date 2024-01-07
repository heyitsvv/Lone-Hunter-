using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem != null)
        {
            // Set the parent of the dragged item to the inventory slot
            draggableItem.parentAfterDrag = transform;
            dropped.transform.SetParent(transform);

            // Additional logic for handling the drop, if needed
        }
    }
}
