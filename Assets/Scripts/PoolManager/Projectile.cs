using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f; // Tiempo antes de regresar al pool

    void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // L�gica de da�o al enemigo aqu�
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        CancelInvoke();
        ProjectilePool.instance.ReturnProjectile(gameObject);
    }
}
