using UnityEngine;
using UnityEngine.UI; // Assurez-vous d'ajouter ceci si vous utilisez des composants UI
using UnityEngine.SceneManagement; // Pour recharger la sc�ne ou changer de sc�ne

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Image healthBar; // UISprite pour la barre de vie
    public Text gameOverText; // Texte pour Game Over

    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
        gameOverText.gameObject.SetActive(false); // Assurez-vous que le texte Game Over est cach� au d�part
        Debug.Log(gameObject.name + " Initial Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " Health after damage: " + currentHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetBool("IsDead", true);
        }
        else
        {
            Destroy(gameObject);
        }

        // Optionnel : Ajouter un d�lai avant de recharger la sc�ne
        Invoke("ReloadScene", 3f); // Attend 3 secondes avant de recharger la sc�ne
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recharger la sc�ne actuelle
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            healthBar.fillAmount = healthPercentage;
        }
    }
}