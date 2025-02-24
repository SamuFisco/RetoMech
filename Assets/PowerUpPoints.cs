using UnityEngine;

public class PowerUpPoints : MonoBehaviour
{
    public int pointsToAdd = 2; // Puntos que se suman al recoger el power up

    // Este método se ejecuta al entrar en colisión con otro collider configurado como trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que colisiona es el jugador (asegúrate de que tenga la etiqueta "Player")
        if (other.CompareTag("Player"))
        {
            // Buscar el TimerManager en la escena para actualizar la puntuación
            TimerManager timerManager = FindObjectOfType<TimerManager>();
            if (timerManager != null)
            {
                timerManager.AddScore(pointsToAdd);
            }
            else
            {
                Debug.LogWarning("No se encontró TimerManager en la escena.");
            }

            // Destruir el power up después de recogerlo
            Destroy(gameObject);
        }
    }
}
