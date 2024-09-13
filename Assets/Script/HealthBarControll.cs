using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public CharacterStats characterStats; // Référence au script CharacterStats
    public Image healthBar; // Référence à l'image de la barre de vie

    void Update()
    {
        if (characterStats != null && healthBar != null)
        {
            // Calculer la fraction de la vie actuelle par rapport à la vie maximale
            float healthFraction = (float)characterStats.currentHealth / characterStats.maxHealth;
            healthBar.fillAmount = healthFraction; // Mettre à jour la barre de vie
        }
    }
}
