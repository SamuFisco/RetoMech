using UnityEngine;

public class RuedaSonido : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource ruedaAudio; // Referencia al AudioSource
    public float minRotationSpeed = 5f; // M�nima velocidad de rotaci�n para reproducir sonido

    private float lastRotationAngle; // �ltimo �ngulo de rotaci�n

    void Start()
    {
        if (ruedaAudio == null)
        {
            ruedaAudio = GetComponent<AudioSource>(); // Intenta encontrar el AudioSource autom�ticamente
        }

        lastRotationAngle = transform.rotation.eulerAngles.x;
    }

    void Update()
    {
        ReproducirSonidoSiGira();
    }

    /// <summary>
    /// Activa o desactiva el sonido de la rueda seg�n su rotaci�n.
    /// </summary>
    private void ReproducirSonidoSiGira()
    {
        // Calculamos la diferencia de rotaci�n en el eje X
        float rotationSpeed = Mathf.Abs(transform.rotation.eulerAngles.x - lastRotationAngle) / Time.deltaTime;
        lastRotationAngle = transform.rotation.eulerAngles.x;

        if (rotationSpeed > minRotationSpeed)
        {
            if (!ruedaAudio.isPlaying)
            {
                ruedaAudio.Play(); // Reproduce el sonido si la rueda est� girando r�pido
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

