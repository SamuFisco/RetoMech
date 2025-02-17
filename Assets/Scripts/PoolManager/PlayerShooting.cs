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

    [Header("Componentes")]
    public Animator animator;
    private ShootingSound shootingSound; // Sonido dentro del Player
    private SoundDisparo soundDisparo;   // Sonido desde el SoundManager

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

            proyectilIzq.GetComponent<Projectile>().direccionDisparo(transform.forward);
            proyectilDer.GetComponent<Projectile>().direccionDisparo(transform.forward);

            // Aplicar velocidad a los proyectiles
            proyectilIzq.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
            proyectilDer.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        }

        // **Esperar el tiempo de recarga antes de permitir otro disparo**
        yield return new WaitForSeconds(tiempoRecarga);
        puedeDisparar = true;
    }
}
