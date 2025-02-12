using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; // Punto de disparo en el jugador
    public float projectileSpeed = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = ProjectilePool.instance.GetProjectile();

        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.GetComponent<Rigidbody>().velocity = firePoint.forward * projectileSpeed;
        }
    }
}
