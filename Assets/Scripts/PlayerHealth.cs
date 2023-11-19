using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject healthBar;
    public Slider slider;
    private Image bar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        slider = healthBar.GetComponent<Slider>();
        bar = healthBar.transform.Find("Bar").GetComponent<Image>();
        bar.color = new Color32(113, 219, 113, 255);
        UpdateHealth();
    }

    // Method to reduce the player's health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealth();
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void UpdateHealth()
    {
        slider.value = currentHealth;
        if (currentHealth <= 20)
        {
            bar.color = Color.red;
        }
        else if (currentHealth <= 50)
        {
            bar.color = new Color32(232, 238, 10, 255);
        }
        else
        {
            bar.color = new Color32(113, 219, 113, 255);
        }
    }
}
