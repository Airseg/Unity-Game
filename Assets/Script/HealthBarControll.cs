using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public CharacterStats characterStats; // R�f�rence au script CharacterStats
    public Image healthBar; // R�f�rence � l'image de la barre de vie

    void Update()
    {
        if (characterStats != null && healthBar != null)
        {
            // Calculer la fraction de la vie actuelle par rapport � la vie maximale
            float healthFraction = (float)characterStats.currentHealth / characterStats.maxHealth;
            healthBar.fillAmount = healthFraction; // Mettre � jour la barre de vie
        }
    }
}
