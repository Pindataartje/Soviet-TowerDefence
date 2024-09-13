using UnityEngine;
using UnityEngine.UI;

public class HPBarTest : MonoBehaviour
{
    public Slider healthBar;
    public float maxHealth = 100f;
    private float currentHealth;

    void Start()
    {
        // Initialize the health bar
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        // Reduce current health by the damage amount
        currentHealth -= damageAmount;

        // Ensure health does not go below 0
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar
        healthBar.value = currentHealth;

        // Check if health has reached 0
        if (currentHealth <= 0)
        {
            Debug.Log("Player is dead!");
            // You can add any additional logic here for when the player dies
        }
    }

    // This method is just for testing purposes, to simulate taking damage
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Simulate taking 10 damage when the space key is pressed
            TakeDamage(10f);
        }
    }
}