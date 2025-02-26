using UnityEngine;

public class EnemyProximityAlert : MonoBehaviour
{
    public float detectionRange = 10f; // Rango en el que se detectan enemigos cercanos
    public AudioSource alarmAudioSource; // Componente AudioSource que reproducirá el sonido de alarma
    public AudioClip alarmSound; // Clip de audio que se usará como sonido de la alarma
    public GameObject alertIndicator; // Objeto que representa la alerta visual (icono, luz o UI)

    private bool isAlarmPlaying = false; // Variable para controlar si la alarma está activa

    void Start()
    {
        if (alertIndicator != null)
            alertIndicator.SetActive(false); // Desactiva la alerta visual al inicio para que solo aparezca cuando haya un enemigo cerca
    }

    void Update()
    {
        DetectarEnemigos(); // Llama a la función que verifica la proximidad de los enemigos en cada fotograma
    }

    void DetectarEnemigos()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy"); // Obtiene todos los objetos en la escena con la etiqueta "Enemy"
        bool enemigoCercano = false; // Variable para verificar si hay algún enemigo dentro del rango de detección

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position); // Calcula la distancia entre este objeto y cada enemigo
            if (distancia < detectionRange) // Si la distancia es menor que el rango de detección
            {
                enemigoCercano = true; // Se establece que hay un enemigo cercano
                break; // Se detiene el bucle para evitar cálculos innecesarios
            }
        }

        if (enemigoCercano && !isAlarmPlaying) // Si hay un enemigo cerca y la alarma no está sonando
        {
            ActivarAlarma(); // Activa la alarma
        }
        else if (!enemigoCercano && isAlarmPlaying) // Si no hay enemigos cerca y la alarma está sonando
        {
            DesactivarAlarma(); // Desactiva la alarma
        }
    }

    void ActivarAlarma()
    {
        if (alarmAudioSource != null && alarmSound != null) // Verifica que haya un AudioSource y un sonido asignado antes de intentar reproducirlo
        {
            alarmAudioSource.clip = alarmSound; // Asigna el clip de sonido al AudioSource
            alarmAudioSource.loop = true; // Configura la alarma para que se reproduzca en bucle
            alarmAudioSource.Play(); // Reproduce el sonido de la alarma
            isAlarmPlaying = true; // Marca la alarma como activa
            Debug.Log("Alarma activada: Enemigo cerca."); // Mensaje de depuración indicando que la alarma se ha activado
        }
        else
        {
            Debug.LogError("No se ha asignado el AudioSource o el sonido de la alarma."); // Mensaje de error si no se asignaron los componentes de audio
        }

        if (alertIndicator != null)
            alertIndicator.SetActive(true); // Activa el indicador visual de alerta si está asignado
    }

    void DesactivarAlarma()
    {
        if (alarmAudioSource != null) // Verifica que el AudioSource no sea nulo antes de intentar detenerlo
        {
            alarmAudioSource.Stop(); // Detiene la reproducción del sonido de alarma
            isAlarmPlaying = false; // Marca la alarma como desactivada
            Debug.Log("Alarma desactivada: Enemigo fuera de rango."); // Mensaje de depuración indicando que la alarma se ha desactivado
        }

        if (alertIndicator != null)
            alertIndicator.SetActive(false); // Desactiva el indicador visual de alerta si está asignado
    }
}
