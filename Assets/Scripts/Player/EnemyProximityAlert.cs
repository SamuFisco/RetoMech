using UnityEngine;

public class EnemyProximityAlert : MonoBehaviour
{
    public float detectionRange = 10f; // Rango de detección del enemigo
    public AudioSource alarmAudioSource; // ✅ AudioSource para la alarma
    public AudioClip alarmSound; // ✅ Sonido de la alarma
    public GameObject alertIndicator; // ✅ Objeto rojo de alerta (Icono, Luz, UI)

    private bool isAlarmPlaying = false;

    void Start()
    {
        if (alertIndicator != null)
            alertIndicator.SetActive(false); // ✅ Asegurar que la alerta inicia desactivada
    }

    void Update()
    {
        DetectarEnemigos();
    }

    void DetectarEnemigos()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        bool enemigoCercano = false;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < detectionRange)
            {
                enemigoCercano = true;
                break;
            }
        }

        if (enemigoCercano && !isAlarmPlaying)
        {
            ActivarAlarma();
        }
        else if (!enemigoCercano && isAlarmPlaying)
        {
            DesactivarAlarma();
        }
    }

    void ActivarAlarma()
    {
        if (alarmAudioSource != null && alarmSound != null)
        {
            alarmAudioSource.clip = alarmSound;
            alarmAudioSource.loop = true;
            alarmAudioSource.Play();
            isAlarmPlaying = true;
            Debug.Log("🚨 Alarma activada: Enemigo cerca.");
        }
        else
        {
            Debug.LogError("⚠ No se ha asignado el AudioSource o el sonido de la alarma.");
        }

        if (alertIndicator != null)
            alertIndicator.SetActive(true); // ✅ Activar la alerta roja
    }

    void DesactivarAlarma()
    {
        if (alarmAudioSource != null)
        {
            alarmAudioSource.Stop();
            isAlarmPlaying = false;
            Debug.Log("🔇 Alarma desactivada: Enemigo fuera de rango.");
        }

        if (alertIndicator != null)
            alertIndicator.SetActive(false); // ✅ Apagar la alerta roja
    }
}

