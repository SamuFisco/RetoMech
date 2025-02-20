using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyVision : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float detectionRange = 10f; // Distancia de detecci�n
    public float fieldOfView = 60f; // �ngulo de visi�n del enemigo
    public LayerMask obstaclesMask; // Define qu� capas bloquean la vista
    public GameObject gameOverUI; // Panel de Game Over
    private NavMeshAgent agent; // Agente de patrullaje (si aplica)

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // Obtiene el NavMeshAgent
        gameOverUI.SetActive(false); // Asegura que el Game Over est� oculto al inicio
    }

    void Update()
    {
        DetectarJugador();
    }

    void DetectarJugador()
    {
        if (player == null) return;

        // Calcular direcci�n hacia el jugador
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Verificar si el jugador est� dentro del campo de visi�n y rango de detecci�n
        if (Vector3.Angle(transform.forward, directionToPlayer) < fieldOfView / 2 && distanceToPlayer <= detectionRange)
        {
            // Realizar un Raycast desde el enemigo hacia el jugador
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, directionToPlayer, out hit, detectionRange, ~obstaclesMask))
            {
                if (hit.collider.CompareTag("Player")) // Si impacta al jugador
                {
                    ActivarGameOver();
                }
            }
        }
    }

    void ActivarGameOver()
    {
        Debug.Log("�El enemigo te ha detectado! Game Over.");
        gameOverUI.SetActive(true); // Mostrar pantalla de Game Over
        Time.timeScale = 0; // Pausar el juego
        if (agent != null) agent.isStopped = true; // Detener al enemigo si tiene NavMeshAgent
    }
}
