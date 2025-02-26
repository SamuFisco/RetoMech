using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance; // Instancia única para acceder al pool desde otros scripts
    public GameObject projectilePrefab; // Prefab del proyectil que será instanciado en el pool
    public int poolSize = 10; // Cantidad de proyectiles que se almacenarán en la pool

    private Queue<GameObject> projectilePool = new Queue<GameObject>(); // Cola que almacena los proyectiles disponibles

    void Awake()
    {
        if (instance == null) // Verifica si no hay otra instancia de la clase
        {
            instance = this; // Asigna esta instancia como la única existente
        }
    }

    void Start()
    {
        for (int i = 0; i < poolSize; i++) // Itera hasta alcanzar el tamaño del pool
        {
            GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, -90)); // Crea un proyectil en la posición del pool con rotación específica
            proj.SetActive(false); // Desactiva el proyectil para que no sea visible ni interactúe hasta que se necesite
            projectilePool.Enqueue(proj); // Agrega el proyectil a la cola de disponibles
        }
    }

    public GameObject GetProjectile()
    {
        if (projectilePool.Count > 0) // Verifica si hay proyectiles disponibles en la cola
        {
            GameObject proj = projectilePool.Dequeue(); // Extrae el primer proyectil disponible
            proj.SetActive(true); // Activa el proyectil para su uso
            return proj; // Retorna el proyectil para ser utilizado en el juego
        }
        return null; // Retorna nulo si no hay proyectiles disponibles en la pool
    }

    public void ReturnProjectile(GameObject proj)
    {
        proj.SetActive(false); // Desactiva el proyectil para que no afecte el rendimiento del juego
        projectilePool.Enqueue(proj); // Lo devuelve a la cola de proyectiles disponibles para ser reutilizado
    }
}
