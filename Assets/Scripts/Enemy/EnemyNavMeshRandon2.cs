using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshRandom2 : MonoBehaviour
{
    [Header("Movimiento")]
    public float rangoMovimiento = 10f; // Distancia máxima de movimiento aleatorio
    public float walkSpeed = 2f; // Velocidad al caminar
    public float runSpeed = 4f; // Velocidad al correr
    public float stopThreshold = 0.5f; // Umbral para considerar que llegó al destino

    [Header("Animación")]
    public Animator animator;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Busca el Animator automáticamente
        }

        MoverAUnPuntoAleatorio();
    }

    void Update()
    {
        // Cambia la animación dependiendo de la velocidad
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f)
        {
            if (speed > walkSpeed) animator.SetBool("IsRunning", true);
            else animator.SetBool("IsRunning", false);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }

        // Si el enemigo llegó a su destino, genera un nuevo punto de movimiento
        if (!agent.pathPending && agent.remainingDistance < stopThreshold)
        {
            MoverAUnPuntoAleatorio();
        }
    }

    void MoverAUnPuntoAleatorio()
    {
        Vector3 puntoDestino;
        if (ObtenerPuntoAleatorio(transform.position, rangoMovimiento, out puntoDestino))
        {
            agent.SetDestination(puntoDestino);

            // Aleatoriamente decide si camina o corre
            if (Random.value > 0.5f)
            {
                agent.speed = runSpeed;
                animator.SetBool("IsRunning", true);
            }
            else
            {
                agent.speed = walkSpeed;
                animator.SetBool("IsRunning", false);
            }
        }
    }

    bool ObtenerPuntoAleatorio(Vector3 origen, float rango, out Vector3 resultado)
    {
        Vector3 puntoRandom = origen + Random.insideUnitSphere * rango;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(puntoRandom, out hit, rango, NavMesh.AllAreas))
        {
            resultado = hit.position;
            return true;
        }

        resultado = Vector3.zero;
        return false;
    }
}
