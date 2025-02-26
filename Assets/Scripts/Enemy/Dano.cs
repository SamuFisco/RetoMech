using UnityEngine;
using System.Collections;

public class Dano : MonoBehaviour
{
    public int vida = 2; // Cantidad de vida que tiene el enemigo antes de ser eliminado
    public GameObject efectoMuerte; // Prefab del efecto visual que se activará al morir
    public AudioSource audioSource; // Componente AudioSource que reproducirá el sonido de explosión
    public AudioClip explosionSound; // Clip de sonido de la explosión al ser destruido

    private bool isDead = false; // Variable para evitar múltiples ejecuciones al recibir daño

    void Start()
    {
        // Asigna el componente AudioSource si no ha sido asignado previamente
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) // Verifica si el objeto no tiene un AudioSource adjunto
        {
            Debug.LogError("No se encontró un AudioSource en " + gameObject.name); // Mensaje de error si falta el componente
        }
    }

    public void RecibirImpacto()
    {
        if (isDead) return; // Si el enemigo ya está marcado como eliminado, no recibe más daño

        vida--; // Reduce la vida del enemigo en 1
        Debug.Log("Enemy impactado, vida restante: " + vida); // Mensaje en consola con la vida restante

        if (vida <= 0) // Si la vida llega a 0 o menos, se ejecuta el proceso de eliminación
        {
            ApagarEnemigo(); // Llama a la función para eliminar al enemigo
        }
    }

    void ApagarEnemigo()
    {
        if (isDead) return; // Evita que el enemigo se elimine más de una vez
        isDead = true; // Marca al enemigo como eliminado

        Debug.Log("Enemy eliminado"); // Mensaje en consola indicando que el enemigo ha sido eliminado

        if (explosionSound != null && audioSource != null) // Verifica que haya un sonido y un AudioSource asignado
        {
            audioSource.PlayOneShot(explosionSound, 1.0f); // Reproduce el sonido de explosión
            Debug.Log("Sonido de explosión reproducido."); // Mensaje de confirmación en consola
        }
        else
        {
            Debug.LogError("No se ha asignado un sonido de explosión o falta AudioSource."); // Mensaje de error si faltan elementos de audio
        }

        if (efectoMuerte != null) // Verifica si hay un efecto de muerte asignado
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity); // Instancia el efecto de explosión en la posición del enemigo
            Debug.Log("Efecto de explosión instanciado."); // Mensaje de confirmación en consola
        }

        TimerManager timer = FindObjectOfType<TimerManager>(); // Busca un objeto TimerManager en la escena
        if (timer != null) // Si se encuentra un TimerManager en la escena
        {
            timer.EnemyDefeated(); // Llama a la función EnemyDefeated para actualizar el temporizador o la puntuación
        }

        StartCoroutine(DesactivarTrasEfecto()); // Inicia la corrutina para desactivar el enemigo tras un corto tiempo
    }

    IEnumerator DesactivarTrasEfecto()
    {
        yield return new WaitForSeconds(0.1f); // Espera 0.1 segundos antes de ejecutar la siguiente acción
        gameObject.SetActive(false); // Desactiva el enemigo en la escena en lugar de destruirlo
    }
}
