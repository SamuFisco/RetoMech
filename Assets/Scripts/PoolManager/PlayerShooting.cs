using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public Transform cañonIzquierdo;  // Punto de disparo del cañón izquierdo
    public Transform cañonDerecho;   // Punto de disparo del cañón derecho
    public float projectileSpeed = 20f;
    public float tiempoRecarga = 1f; // Tiempo entre disparos
    private bool puedeDisparar = true;

    public Animator animator; // Referencia a la animación de disparo

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && puedeDisparar)
        {
            StartCoroutine(Disparar());
        }
    }

    IEnumerator Disparar()
    {
        puedeDisparar = false;

        // Activar animación de disparo
        if (animator != null)
        {
            animator.SetTrigger("Disparar");
        }

        // Obtener proyectiles del pool
        GameObject proyectilIzq = ProjectilePool.instance.GetProjectile();
        GameObject proyectilDer = ProjectilePool.instance.GetProjectile();

        if (proyectilIzq != null && proyectilDer != null)
        {
            // Posicionar los proyectiles en los cañones
            proyectilIzq.transform.position = cañonIzquierdo.position;
            proyectilIzq.transform.rotation = cañonIzquierdo.rotation;

            proyectilDer.transform.position = cañonDerecho.position;
            proyectilDer.transform.rotation = cañonDerecho.rotation;

            // Asegurar que los proyectiles disparan hacia adelante
            proyectilIzq.GetComponent<Rigidbody>().velocity = cañonIzquierdo.transform.forward * projectileSpeed;
            proyectilDer.GetComponent<Rigidbody>().velocity = cañonDerecho.transform.forward * projectileSpeed;
        }

        // Esperar el tiempo de recarga antes de permitir otro disparo
        yield return new WaitForSeconds(tiempoRecarga);
        puedeDisparar = true;
    }
}
