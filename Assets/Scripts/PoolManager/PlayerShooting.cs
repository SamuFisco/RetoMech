using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public Transform cañonIzquierdo;
    public Transform cañonDerecho;
    public float projectileSpeed = 20f;
    public float tiempoRecarga = 1f;
    private bool puedeDisparar = true;

    [Header("Efectos de Partículas")]
    public ParticleSystem particulasCañonIzq;
    public ParticleSystem particulasCañonDer;

    [Header("Componentes")]
    public Animator animator;
    private ShootingSound shootingSound;
    private SoundDisparo soundDisparo;
    private TimerManager timerManager; // ✅ Usamos TimerManager para manejar la puntuación

    void Start()
    {
        shootingSound = GetComponent<ShootingSound>();

        GameObject soundManager = GameObject.Find("SoundManager");
        if (soundManager != null)
        {
            soundDisparo = soundManager.GetComponent<SoundDisparo>();
        }
        else
        {
            Debug.LogError("❌ No se encontró SoundManager en la escena.");
        }

        // 🔹 Buscar `TimerManager` en TODA la escena (porque está en un objeto vacío separado)
        timerManager = FindObjectOfType<TimerManager>();

        if (timerManager == null)
        {
            Debug.LogError("❌ No se encontró TimerManager en la escena. Asegúrate de que existe y está activo.");
        }
        else
        {
            Debug.Log("✅ TimerManager encontrado correctamente.");
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && puedeDisparar)
        {
            StartCoroutine(Disparar());
        }
    }

    IEnumerator Disparar()
    {
        puedeDisparar = false;

        if (animator != null)
        {
            animator.SetTrigger("Disparar");
        }

        shootingSound?.PlayShootSound();
        soundDisparo?.PlayShootSound();

        particulasCañonIzq?.Play();
        particulasCañonDer?.Play();

        GameObject proyectilIzq = ProjectilePool.instance?.GetProjectile();
        GameObject proyectilDer = ProjectilePool.instance?.GetProjectile();

        if (proyectilIzq == null || proyectilDer == null)
        {
            Debug.LogWarning("⚠ No hay proyectiles en el Pool.");
            yield return new WaitForSeconds(tiempoRecarga);
            puedeDisparar = true;
            yield break;
        }

        proyectilIzq.transform.position = cañonIzquierdo.position;
        proyectilIzq.transform.rotation = cañonIzquierdo.rotation;

        proyectilDer.transform.position = cañonDerecho.position;
        proyectilDer.transform.rotation = cañonDerecho.rotation;

        proyectilIzq.GetComponent<Projectile>().direccionDisparo(transform.forward);
        proyectilDer.GetComponent<Projectile>().direccionDisparo(transform.forward);

        proyectilIzq.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        proyectilDer.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;

        // ✅ Verificar impactos en enemigos
        bool impactado = false;

        Collider[] hitsIzq = Physics.OverlapSphere(proyectilIzq.transform.position, 0.5f);
        Collider[] hitsDer = Physics.OverlapSphere(proyectilDer.transform.position, 0.5f);

        foreach (Collider hit in hitsIzq)
        {
            if (hit.CompareTag("Enemy"))
            {
                impactado = true;
                timerManager?.AddScore(2); // ✅ SUMAR PUNTOS SI IMPACTA
                break;
            }
        }

        foreach (Collider hit in hitsDer)
        {
            if (hit.CompareTag("Enemy"))
            {
                impactado = true;
                timerManager?.AddScore(2); // ✅ SUMAR PUNTOS SI IMPACTA
                break;
            }
        }

        if (!impactado)
        {
            timerManager?.AddScore(-2); // ❌ RESTAR PUNTOS SI FALLA
        }

        yield return new WaitForSeconds(tiempoRecarga);
        puedeDisparar = true;
    }
}
