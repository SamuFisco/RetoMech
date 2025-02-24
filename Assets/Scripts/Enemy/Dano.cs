using UnityEngine;
using System.Collections;

public class Dano : MonoBehaviour
{
    public int vida = 2;
    public GameObject efectoMuerte;
    public AudioSource audioSource; // ✅ Nuevo componente AudioSource
    public AudioClip explosionSound;

    private bool isDead = false;

    void Start()
    {
        // ✅ Asegurar que el AudioSource está asignado
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("⚠ No se encontró un AudioSource en " + gameObject.name);
        }
    }

    public void RecibirImpacto()
    {
        if (isDead) return;

        vida--;
        Debug.Log("Enemy impactado, vida restante: " + vida);

        if (vida <= 0)
        {
            ApagarEnemigo();
        }
    }

    void ApagarEnemigo()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("❌ Enemy eliminado");

        if (explosionSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(explosionSound, 1.0f); // ✅ Reproduce el sonido
            Debug.Log("🔊 Sonido de explosión reproducido.");
        }
        else
        {
            Debug.LogError("⚠ No se ha asignado un sonido de explosión o falta AudioSource.");
        }

        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
            Debug.Log("💥 WFX instanciado.");
        }

        TimerManager timer = FindObjectOfType<TimerManager>();
        if (timer != null)
        {
            timer.EnemyDefeated();
        }

        StartCoroutine(DesactivarTrasEfecto());
    }

    IEnumerator DesactivarTrasEfecto()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
