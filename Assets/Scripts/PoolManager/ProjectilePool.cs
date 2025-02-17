using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public static ProjectilePool instance;
    public GameObject projectilePrefab;
    public int poolSize = 10;

    private Queue<GameObject> projectilePool = new Queue<GameObject>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // Crear los 10 proyectiles y desactivarlos
        for (int i = 0; i < poolSize; i++)
        {
            GameObject proj = Instantiate(projectilePrefab,transform.position,Quaternion.Euler(0,0,-90));
            proj.SetActive(false);
            
            projectilePool.Enqueue(proj);
        }
    }

    public GameObject GetProjectile()
    {
        if (projectilePool.Count > 0)
        {
            GameObject proj = projectilePool.Dequeue();
            proj.SetActive(true);
            return proj;
        }
        return null;
    }

    public void ReturnProjectile(GameObject proj)
    {
        proj.SetActive(false);
        projectilePool.Enqueue(proj);
    }
}
