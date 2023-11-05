using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Method to reduce the player's health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player took damage: " + currentHealth);

        // Check if the player's health is less than or equal to zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to handle the player's death
    private void Die()
    {
        // You can implement death logic here, such as showing a game over screen, respawning, etc.
        Debug.Log("Player has died.");
        // For example, reload the current scene:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
