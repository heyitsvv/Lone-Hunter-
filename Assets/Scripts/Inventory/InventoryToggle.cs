using UnityEngine;

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // Drag your InventoryPanel here in the inspector
    private bool isInventoryOpen = false;
    public GunShoot gunShootScript;
    public PlayerLooking playerLookingScript;

    void Start()
    {
        // Initialize with inventory closed
        inventoryPanel.SetActive(false);
        isInventoryOpen = false;
        if (gunShootScript != null && playerLookingScript != null)
        {
            gunShootScript.enabled = true;
            playerLookingScript.enabled = true;
        }
    }
    
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
        ToggleCursorState();

        if (gunShootScript != null && playerLookingScript != null)
        {
            gunShootScript.enabled = !isInventoryOpen;
            playerLookingScript.enabled = !isInventoryOpen;
        }
        else
        {
            Debug.LogError("GunShoot or PlayerLooking script reference is not assigned in InventoryToggle.");
        }
    }

    private void ToggleCursorState()
    {
        if (isInventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
