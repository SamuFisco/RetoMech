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
    private ShootingSound shootingSound; // Sonido dentro del Player
    private SoundDisparo soundDisparo;   // Sonido desde el SoundManager
    private TimerManager timerManager;   // Referencia al sistema de tiempo y puntuación

    void Start()
    {
        // Buscar el script `ShootingSound` en el Player
        shootingSound = GetComponent<ShootingSound>();

        // Buscar `SoundManager` en la escena
        GameObject soundManager = GameObject.Find("SoundManager");
        if (soundManager != null)
        {
            soundDisparo = soundManager.GetComponent<SoundDisparo>();
        }
        else
        {
            Debug.LogError("❌ No se encontró SoundManager en la escena. Asegúrate de crearlo.");
        }

        // Buscar `TimerManager` en la escena
        timerManager = FindObjectOfType<TimerManager>();
        if (timerManager == null)
        {
            Debug.LogError("❌ No se encontró TimerManager en la escena.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && puedeDisparar) // Click Izquierdo para disparar
        {
            StartCoroutine(Disparar());
        }
    }

    IEnumerator Disparar()
    {
        puedeDisparar = false;

        // **Activar animación de disparo**
        if (animator != null)
        {
            animator.SetTrigger("Disparar");
        }

        // **Reproducir sonido dentro del Player**
        if (shootingSound != null)
        {
            shootingSound.PlayShootSound();
        }

        // **Reproducir sonido desde el SoundManager**
        if (soundDisparo != null)
        {
            soundDisparo.PlayShootSound();
        }

        // **Disparar partículas en los cañones exactamente en el momento del disparo**
        if (particulasCañonIzq != null && particulasCañonDer != null)
        {
            particulasCañonIzq.Play();
            particulasCañonDer.Play();
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

            // Aplicar dirección y velocidad a los proyectiles
            proyectilIzq.GetComponent<Projectile>().direccionDisparo(transform.forward);
            proyectilDer.GetComponent<Projectile>().direccionDisparo(transform.forward);

            proyectilIzq.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            proyectilDer.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;

            // **Verificar si impactan en un enemigo, si no, restar puntos**
            bool impactado = false;
            Collider[] hits = Physics.OverlapSphere(proyectilIzq.transform.position, 0.5f);
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    impactado = true;
                    break;
                }
            }

            if (!impactado && timerManager != null)
            {
                timerManager.AddScore(-2); // Restar puntos si el disparo falló
            }
        }

        // **Esperar el tiempo de recarga antes de permitir otro disparo**
        yield return new WaitForSeconds(tiempoRecarga);

        puedeDisparar = true;
    }
}
