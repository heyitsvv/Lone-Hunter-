using UnityEngine;
using System.Collections;



public class GatherableItem : MonoBehaviour
{
    private bool isGathered = false;

    void Start()
    {
        // Disable the collider initially
        GetComponent<Collider>().enabled = false;
    }

    public void DropToGround(Vector3 position)
    {
        // Set the position and enable the collider when dropped to the ground
        transform.position = position;
        GetComponent<Collider>().enabled = true;

        // You can add additional effects or animations here

        Debug.Log("Item dropped to ground at position: " + position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGathered)
            return;

        // Check if the player entered the collider
        if (other.CompareTag("Player"))
        {
            Gather(other.GetComponent<InventoryToggle>());
        }
    }

    private void Gather(InventoryToggle inventoryToggle)
    {
        if (inventoryToggle != null)
        {
            // Toggle the inventory to show the gathered item
            inventoryToggle.ToggleInventory();

            // You can add the gathered item to the inventory here
            // For example, you might instantiate an item in the inventory UI
            // and set its icon or update a counter for the item.

            isGathered = true;

            // You can add additional effects or animations here

            // Destroy the gatherable object after it's gathered
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
        else
        {
            Debug.LogError("InventoryToggle reference not found.");
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
