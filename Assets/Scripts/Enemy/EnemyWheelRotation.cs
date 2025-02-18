using UnityEngine;
using UnityEngine.AI;

public class EnemyWheelRotation : MonoBehaviour
{
    public Transform wheel; // Referencia a la rueda
    public float wheelRadius = 0.5f; // Ajustar según el tamaño de la rueda
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (wheel == null)
        {
            Debug.LogError("No se ha asignado la rueda al enemigo.");
        }
    }

    void Update()
    {
        if (agent.velocity.magnitude > 0.1f) // Solo rota si se mueve
        {
            float distanceMoved = agent.velocity.magnitude * Time.deltaTime; // Distancia recorrida
            float rotationAmount = (distanceMoved / (2 * Mathf.PI * wheelRadius)) * 360; // Convertir a grados
            wheel.Rotate(Vector3.right, rotationAmount);
        }
    }
}
