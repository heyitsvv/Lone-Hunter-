using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private bool dead = false;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && !dead)
        {
            Die();
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        return dead;
    }

    private void Die()
    {
        dead = true;
    }
}
