using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Joueur a re�u " + amount + " d�g�ts, vie restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Joueur est mort !");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Rechargement de la sc�ne actuelle
    }
}
