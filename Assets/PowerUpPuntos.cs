using UnityEngine;

public class PowerUpPuntos : MonoBehaviour
{
    public int pointsToAdd = 2; // Puntos que se suman al recoger el power-up
    public AudioClip powerUpSound; // Sonido a reproducir
    private AudioSource audioSource;

    private void Start()
    {
        // Buscar o añadir un AudioSource automáticamente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //Iniciar el efecto de "saltito" con LeanTween
        StartJumping();
    }

    private void StartJumping()
    {
        float originalY = transform.position.y;

        //Movimiento en 3D: solo afectamos la posición en Y
        LeanTween.moveY(gameObject, originalY + 0.3f, 0.5f)
                 .setEaseInOutSine() //Suaviza el movimiento
                 .setLoopPingPong(); // Hace que el movimiento sea continuo
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PowerUpPuntos recogido: +2 puntos añadidos.");

            // Agregar puntos al TimerManager
            TimerManager timerManager = FindObjectOfType<TimerManager>();
            if (timerManager != null)
            {
                timerManager.AddScore(pointsToAdd);
            }
            else
            {
                Debug.LogWarning("No se encontró TimerManager en la escena.");
            }

            // 🔊 Reproducir sonido antes de destruir el Power-Up
            if (powerUpSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(powerUpSound);
                Debug.Log("🔊 Reproduciendo sonido del Power-Up.");
                Destroy(gameObject, powerUpSound.length); // Esperar a que termine el sonido antes de destruir
            }
            else
            {
                Debug.LogWarning("No hay sonido asignado o AudioSource es NULL.");
                Destroy(gameObject); // Si no hay sonido, destruir inmediatamente
            }
        }
    }
}
