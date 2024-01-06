using UnityEngine;

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

        Debug.Log("Item dropped to ground at position: " + position);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isGathered)
            return;

        // Check if the player entered the collider
        if (other.CompareTag("Player"))
        {
            Gather(other.GetComponent<InventoryToggle>());
        }
    }

    public void Gather(InventoryToggle inventoryToggle)
    {
        if (inventoryToggle != null)
        {

            inventoryToggle.ToggleInventory();
            

            isGathered = true;
            StartCoroutine(DestroyAfterDelay(0.5f));
        }
        else
        {
            Debug.LogError("InventoryToggle reference not found.");
        }
    }

    private System.Collections.IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
