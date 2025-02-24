using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f; // Tiempo antes de regresar al pool
    public float speed = 20f; // Velocidad base del proyectil
    public float trackingStrength = 10f; // ✅ Ajustar para más seguimiento
    public float maxRotationSpeed = 360f; // ✅ Velocidad máxima de giro

    public GameObject impactEffect; // ✅ Prefab de efecto de impacto
    public AudioSource audioSource;
    public AudioClip impactSound;

    private Transform targetEnemy; // ✅ Referencia al enemigo más cercano
    private Vector3 _dir;
    private bool isTracking = false;

    void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifeTime);
        BuscarEnemigoMasCercano(); // ✅ Buscar enemigo al activarse

        if (targetEnemy != null)
        {
            isTracking = true;
        }
    }

    void FixedUpdate()
    {
        if (targetEnemy != null && isTracking)
        {
            // ✅ Calcular la dirección hacia el enemigo
            Vector3 dirToTarget = (targetEnemy.position - transform.position).normalized;

            // ✅ Girar gradualmente hacia el enemigo usando RotateTowards
            _dir = Vector3.RotateTowards(_dir, dirToTarget, maxRotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
        }

        // ✅ Aplicar movimiento del proyectil
        transform.position += _dir * speed * Time.deltaTime;
    }

    public void direccionDisparo(Vector3 dir)
    {
        _dir = dir;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // ✅ Aplicar daño al enemigo
            Dano enemigoDano = other.GetComponent<Dano>();
            if (enemigoDano != null)
            {
                enemigoDano.RecibirImpacto();
            }

            // ✅ Sumar 2 puntos al impactar un enemigo
            TimerManager timer = FindObjectOfType<TimerManager>();
            if (timer != null)
            {
                timer.AddScore(2);
            }

            ReturnToPool();
        }
        else
        {
            // ✅ Si impacta contra otra cosa, mostrar efecto
            StartCoroutine(ImpactEffectCoroutine());
        }
    }

    IEnumerator ImpactEffectCoroutine()
    {
        Debug.Log("💥 Proyectil impactó contra un objeto no enemigo.");

        // ✅ Reproducir sonido
        if (impactSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(impactSound);
        }

        // ✅ Mostrar efecto de impacto
        if (impactEffect != null)
        {
            GameObject efecto = Instantiate(impactEffect, transform.position, Quaternion.identity);
            Destroy(efecto, 2f);
        }

        // ✅ Esperar antes de regresar al pool
        yield return new WaitForSeconds(1f);
        ReturnToPool();
    }

    void ReturnToPool()
    {
        CancelInvoke();
        if (ProjectilePool.instance != null)
        {
            ProjectilePool.instance.ReturnProjectile(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void BuscarEnemigoMasCercano()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag("Enemy");
        float menorDistancia = Mathf.Infinity;
        Transform enemigoMasCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);
            if (distancia < menorDistancia)
            {
                menorDistancia = distancia;
                enemigoMasCercano = enemigo.transform;
            }
        }

        targetEnemy = enemigoMasCercano; // ✅ Asigna el enemigo más cercano
    }
}
