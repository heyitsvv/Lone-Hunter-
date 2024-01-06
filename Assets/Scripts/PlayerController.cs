using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float pickUpDistance = 3f; // Adjust the distance based on your needs

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickUpItem();
        }
    }

    private void TryPickUpItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickUpDistance))
        {
            if (hit.collider.CompareTag("GatherableItem"))
            {
                GatherableItem item = hit.collider.GetComponent<GatherableItem>();
                if (item != null)
                {
                    item.Gather(GetComponent<InventoryToggle>());
                }
            }
        }
    }
}
