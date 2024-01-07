using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Item item;
    public Image icon;
    public Inventory inventory;
    public void UpdateSlot()
    {
        if(item != null)
        {
            icon.sprite = item.icon; // No need for GetComponent<Image>() as icon is already an Image
            icon.gameObject.SetActive(true); // Activate the GameObject that the icon is attached to
        }
        else
        {
            icon.gameObject.SetActive(false); // Deactivate the GameObject
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        DraggableItem draggableItem = droppedObject.GetComponent<DraggableItem>();

        if (draggableItem != null)
        {
            InventorySlot sourceSlot = draggableItem.GetOriginalSlot();
            if (sourceSlot != this)
            {
                // Swap items between slots
                Item tempItem = item;
                item = sourceSlot.item;
                sourceSlot.item = tempItem;

                // Update both slots
                UpdateSlot();
                sourceSlot.UpdateSlot();

                // Update the itemList in the Inventory
                inventory.UpdateItemList(this, sourceSlot);
            }
        }
    }
}
