using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshRandon2 : MonoBehaviour
{
    [Header("Movimiento")]
    public Transform[] puntosPatrulla; // Array de 4 puntos de patrulla
    public float stopThreshold = 0.5f; // Umbral para considerar que llegó al destino
    public float walkSpeed = 2f; // Velocidad al caminar
    public float runSpeed = 4f; // Velocidad al correr

    [Header("Animación")]
    public Animator animator;

    private NavMeshAgent agent;
    private int indicePatrulla = 0; // Índice del punto actual de patrulla

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            animator = GetComponent<Animator>(); // Busca el Animator automáticamente
        }

        if (puntosPatrulla.Length > 0)
        {
            MoverAlSiguientePunto();
        }
        else
        {
            Debug.LogError("No hay puntos de patrulla asignados en el inspector.");
        }
    }

    void Update()
    {
        // Cambia la animación dependiendo de la velocidad
        float speed = agent.velocity.magnitude;

        if (speed > 0.1f)
        {
            if (agent.speed > walkSpeed) animator.SetBool("IsRunning", true);
            else animator.SetBool("IsRunning", false);

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);
        }

        // Si llegó al destino, pasar al siguiente punto de patrulla
        if (!agent.pathPending && agent.remainingDistance < stopThreshold)
        {
            MoverAlSiguientePunto();
        }
    }

    void MoverAlSiguientePunto()
    {
        if (puntosPatrulla.Length == 0)
            return;

        // Seleccionar el siguiente punto en la lista
        Transform puntoDestino = puntosPatrulla[indicePatrulla];
        agent.SetDestination(puntoDestino.position);

        // Cambiar la velocidad aleatoriamente (caminando o corriendo)
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

        // Mover al siguiente punto en la lista circularmente
        indicePatrulla = (indicePatrulla + 1) % puntosPatrulla.Length;
    }
}
