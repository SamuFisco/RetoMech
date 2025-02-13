using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f; // Tiempo antes de regresar al pool
    public float speed = 20f;

    void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        CancelInvoke();
        ProjectilePool.instance.ReturnProjectile(gameObject);
    }
}
