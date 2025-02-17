using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 2f; // Tiempo antes de regresar al pool
    public float speed = 20f;
    Vector3 _dir;
    void OnEnable()
    {
        Invoke("ReturnToPool", lifeTime);
    }

    void FixedUpdate()
    {
        // Movimiento del proyectil en la dirección correcta
        transform.position += (_dir * speed * Time.deltaTime);
    }
    public void direccionDisparo(Vector3 dir)
    {
        _dir = dir;
        
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

