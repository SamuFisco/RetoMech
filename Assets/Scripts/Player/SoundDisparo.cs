using UnityEngine;

public class SoundDisparo : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource shootAudio; // AudioSource para el sonido del disparo

    void Start()
    {
        if (shootAudio == null)
        {
            shootAudio = GetComponent<AudioSource>(); // Busca el AudioSource en el objeto vacío
        }
    }

    /// <summary>
    /// Reproduce el sonido de disparo.
    /// </summary>
    public void PlayShootSound()
    {
        if (shootAudio != null)
        {
            shootAudio.Play(); // Reproducir el sonido
        }
        else
        {
            Debug.LogWarning("❌ No hay AudioSource asignado en " + gameObject.name);
        }
    }
}
