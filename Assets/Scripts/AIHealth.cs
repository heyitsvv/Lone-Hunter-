using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    void Die()
    {
        // Implement what happens when the AI dies (e.g., play death animation, remove AI from the scene, etc.).
        Destroy(gameObject);
    }
}
