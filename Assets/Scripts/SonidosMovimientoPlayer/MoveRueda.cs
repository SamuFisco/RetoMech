using UnityEngine;

public class RuedaSonido : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource ruedaAudio; // Referencia al AudioSource
    public float minRotationSpeed = 5f; // Mínima velocidad de rotación para reproducir sonido

    private float lastRotationAngle; // Último ángulo de rotación

    void Start()
    {
        if (ruedaAudio == null)
        {
            ruedaAudio = GetComponent<AudioSource>(); // Intenta encontrar el AudioSource automáticamente
        }

        lastRotationAngle = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        ReproducirSonidoSiGira();
    }

    /// <summary>
    /// Activa o desactiva el sonido de la rueda según su rotación.
    /// </summary>
    private void ReproducirSonidoSiGira()
    {
        // Calculamos la diferencia de rotación en el eje X
        float rotationSpeed = Mathf.Abs(transform.rotation.eulerAngles.x - lastRotationAngle) / Time.deltaTime;
        lastRotationAngle = transform.rotation.eulerAngles.x;

        if (rotationSpeed > minRotationSpeed)
        {
            if (!ruedaAudio.isPlaying)
            {
                ruedaAudio.Play(); // Reproduce el sonido si la rueda está girando rápido
            }
        }
        else
        {
            if (ruedaAudio.isPlaying)
            {
                ruedaAudio.Stop(); // Detiene el sonido si la rueda deja de girar
            }
        }
    }
}

