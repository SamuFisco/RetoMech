using UnityEngine;
using System.Collections;

public class Dano : MonoBehaviour
{
    public int vida = 2; // Vida del enemigo antes de ser eliminado
    public GameObject efectoMuerte; // Prefab del efecto visual de explosión
    public AudioSource audioSource; // AudioSource del enemigo
    public AudioClip explosionSound; // Sonido de explosión
    public GameObject powerUpPrefab; // Prefab del PowerUp que puede aparecer

    private bool isDead = false; // Evita múltiples ejecuciones al recibir daño

    void Start()
    {
        // Asigna el componente AudioSource si no ha sido asignado previamente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource en " + gameObject.name);
        }
    }

    public void RecibirImpacto()
    {
        if (isDead) return; // Si el enemigo ya está muerto, no recibe daño

        vida--; // Reduce la vida del enemigo
        Debug.Log("Enemy impactado, vida restante: " + vida);

        if (vida <= 0) // Si la vida llega a 0, se elimina el enemigo
        {
            ApagarEnemigo();
        }
    }

    void ApagarEnemigo()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Enemy eliminado");

        // Reproduce sonido de explosión si está asignado
        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound, 1.0f);
            Debug.Log("Sonido de explosión reproducido.");
        }
        else
        {
            Debug.LogError("No se ha asignado un sonido de explosión o falta AudioSource.");
        }

        // Instancia el efecto de explosión si está asignado
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
            Debug.Log("Efecto de explosión instanciado.");
        }

        // Llama a TimerManager si está presente en la escena
        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null)
        {
            timer.EnemyDefeated();
        }

        // Intenta generar un PowerUp con una probabilidad del 30%
        if (powerUpPrefab != null && Random.value <= 100f)
        {
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            Debug.Log("PowerUp generado.");
        }

        StartCoroutine(DesactivarTrasEfecto());
    }

    IEnumerator DesactivarTrasEfecto()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
