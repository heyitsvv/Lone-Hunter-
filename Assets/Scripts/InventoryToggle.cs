using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // Drag your InventoryPanel here in the inspector
    private bool isInventoryOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
    }
}
