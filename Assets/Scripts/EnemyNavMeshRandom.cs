using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMeshRandom : MonoBehaviour
{
    public float rangoMovimiento = 10f; // Distancia máxima donde se moverá aleatoriamente
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoverAUnPuntoAleatorio();
    }

    void Update()
    {
        // Si el enemigo llegó a su destino, genera un nuevo punto de movimiento
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
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
