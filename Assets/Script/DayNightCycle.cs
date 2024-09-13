using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float cycleDuration = 60f; // Durée du cycle complet (en secondes)
    public float dayIntensity = 1f;
    public float nightIntensity = 0.2f;
    public float timeSpeed = 1f; // Facteur d'accélération du temps

    private float timeOfDay;

    void Update()
    {
        // Augmenter le temps de la journée en fonction du facteur de vitesse
        timeOfDay += Time.deltaTime / (cycleDuration / timeSpeed);

        // Boucler le temps de la journée
        if (timeOfDay >= 1f)
        {
            timeOfDay = 0f;
        }

        // Calculer la rotation de la lumière
        float sunAngle = timeOfDay * 360f;
        directionalLight.transform.rotation = Quaternion.Euler(new Vector3(sunAngle - 90f, 170f, 0f));

        // Ajuster l'intensité de la lumière
        float intensity = Mathf.Lerp(nightIntensity, dayIntensity, Mathf.Cos(timeOfDay * Mathf.PI * 2f));
        directionalLight.intensity = intensity;
    }
}
