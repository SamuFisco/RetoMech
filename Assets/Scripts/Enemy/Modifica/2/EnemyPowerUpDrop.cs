using UnityEngine;

/// Este script maneja la lógica de un enemigo que puede soltar un power-up al ser destruido.

public class EnemyPowerUpDrop : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab; // Prefab del power-up que se instanciará.
    [SerializeField] private float dropChance = 100f;  // Probabilidad del 10% de soltar el power-up.

    /// Método que se llama cuando el enemigo es destruido.
    public void OnEnemyDestroyed()
    {
        Debug.Log("OnEnemyDestroyed() llamado en: " + gameObject.name);

        // Determina aleatoriamente si se soltará un power-up (10% de probabilidad).
        float randomValue = Random.Range(0f, 100f);
        Debug.Log("Valor aleatorio: " + randomValue + " - Probabilidad de drop: " + dropChance);

        if (randomValue < dropChance)
        {
            if (powerUpPrefab != null)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
                Debug.Log("Power-up instanciado en: " + transform.position);
            }
            else
            {
                Debug.LogWarning("powerUpPrefab no asignado en el Inspector.");
            }
        }
        else
        {
            Debug.Log("No se generó power-up.");
        }

        // Destruye el enemigo
        Destroy(gameObject);
    }
}
