using UnityEngine;

public class ShootingSound : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource shootAudio; // AudioSource del sonido de disparo

    void Start()
    {
        if (shootAudio == null)
        {
            shootAudio = GetComponent<AudioSource>(); // Busca el AudioSource en el Player
        }
    }

    public void PlayShootSound()
    {
        if (shootAudio != null)
        {
            shootAudio.Play(); // Reproduce el sonido
        }
        else
        {
            Debug.LogWarning("❌ No hay AudioSource asignado en " + gameObject.name);
        }
    }
}
