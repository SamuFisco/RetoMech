using UnityEngine;

public class PowerUpTime : MonoBehaviour
{
    // Tiempo extra en segundos; 60 segundos equivale a 1 minuto
    public float extraTime = 60f;

    // Se ejecuta cuando otro collider entra en contacto (asegúrate de tener Is Trigger activado)
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que colisiona es el jugador (con la etiqueta "Player")
        if (other.CompareTag("Player"))
        {
            // Busca el TimerManager en la escena
            TimerManager timerManager = FindObjectOfType<TimerManager>();
            if (timerManager != null)
            {
                // Suma el tiempo extra al temporizador
                timerManager.timeRemaining += extraTime;
                Debug.Log("PowerUpTime activado: +1 minuto añadido.");
            }
            else
            {
                Debug.LogWarning("No se encontró TimerManager en la escena.");
            }
            // Destruye el power up después de recogerlo
            Destroy(gameObject);
        }
    }
}
