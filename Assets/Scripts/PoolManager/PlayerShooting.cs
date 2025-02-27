using UnityEngine;
using System.Collections;
using StarterAssets;

public class PlayerShooting : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public Transform cañonIzquierdo;
    public Transform cañonDerecho;
    public float projectileSpeed = 20f;
    public float tiempoRecarga = 1f;
    private bool puedeDisparar = true;
    private StarterAssetsInputs input;

    [Header("Efectos de Partículas")]
    public ParticleSystem particulasCañonIzq;
    public ParticleSystem particulasCañonDer;

    [Header("Componentes")]
    public Animator animator;
    private ShootingSound shootingSound;
    private SoundDisparo soundDisparo;
    private TimerManager timerManager; // Se utiliza para actualizar la puntuación
    private ShakeEffect shakeEffect; //Nuevo: Para el temblor de cámara con LeanTween

    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        shootingSound = GetComponent<ShootingSound>();

        GameObject soundManager = GameObject.Find("SoundManager");
        if (soundManager != null)
        {
            soundDisparo = soundManager.GetComponent<SoundDisparo>();
        }
        else
        {
            Debug.LogError("No se encontró SoundManager en la escena.");
        }

        // Buscar TimerManager en toda la escena (asegúrate de tenerlo en un GameObject activo)
        timerManager = FindObjectOfType<TimerManager>();
        if (timerManager == null)
        {
            Debug.LogError("No se encontró TimerManager en la escena. Asegúrate de que existe y está activo.");
        }
        else
        {
            Debug.Log("TimerManager encontrado correctamente.");
        }

        //Nuevo: Buscar ShakeEffect en la escena
        shakeEffect = FindObjectOfType<ShakeEffect>();
        if (shakeEffect == null)
        {
            Debug.LogError("No se encontró ShakeEffect en la escena. Asegúrate de agregarlo a un objeto con la cámara.");
        }
    }

    void Update()
    {
        if (input.disparo && puedeDisparar)
        {
            StartCoroutine(Disparar());
            input.disparo = false;
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

        shakeEffect?.ShakePosition(); //Nuevo: Activar el temblor de cámara con LeanTween

        GameObject proyectilIzq = ProjectilePool.instance?.GetProjectile();
        GameObject proyectilDer = ProjectilePool.instance?.GetProjectile();

        if (proyectilIzq == null || proyectilDer == null)
        {
            Debug.LogWarning("No hay proyectiles en el Pool.");
            yield return new WaitForSeconds(tiempoRecarga);
            puedeDisparar = true;
            yield break;
        }

        // Posicionar y orientar los proyectiles en los cañones correspondientes
        proyectilIzq.transform.position = cañonIzquierdo.position;
        proyectilIzq.transform.rotation = cañonIzquierdo.rotation;

        proyectilDer.transform.position = cañonDerecho.position;
        proyectilDer.transform.rotation = cañonDerecho.rotation;

        proyectilIzq.GetComponent<Projectile>().direccionDisparo(transform.forward);
        proyectilDer.GetComponent<Projectile>().direccionDisparo(transform.forward);

        proyectilIzq.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
        proyectilDer.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;

        // Verificar impactos en enemigos usando un OverlapSphere para cada proyectil
        bool impactado = false;

        Collider[] hitsIzq = Physics.OverlapSphere(proyectilIzq.transform.position, 0.5f);
        Collider[] hitsDer = Physics.OverlapSphere(proyectilDer.transform.position, 0.5f);

        // Si alguno de los proyectiles impacta a un objeto con tag "Enemy", se considera un disparo exitoso.
        foreach (Collider hit in hitsIzq)
        {
            if (hit.CompareTag("Enemy"))
            {
                impactado = true;
                timerManager?.AddScore(2); // Sumar 2 puntos por impactar
                break;
            }
        }

        if (!impactado)
        {
            foreach (Collider hit in hitsDer)
            {
                if (hit.CompareTag("Enemy"))
                {
                    impactado = true;
                    timerManager?.AddScore(2); // Sumar 2 puntos por impactar
                    break;
                }
            }
        }

        // Si ninguno de los proyectiles impacta, se resta 2 puntos (por cada disparo realizado)
        if (!impactado)
        {
            timerManager?.AddScore(-2);
        }

        yield return new WaitForSeconds(tiempoRecarga);
        puedeDisparar = true;
    }
}
