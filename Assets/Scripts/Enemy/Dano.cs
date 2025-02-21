using UnityEngine;
using System.Collections;

public class Dano : MonoBehaviour
{
    public int vida = 2; // ✅ Vida del enemigo (2 impactos antes de apagarse)
    public GameObject efectoMuerte; // ✅ Prefab del WFX (explosión, chispas, etc.)

    void Start()
    {
        // ✅ Asegurarse de que el CapsuleCollider está bien configurado
        CapsuleCollider col = GetComponent<CapsuleCollider>();
        if (col != null)
        {
            col.isTrigger = true; // ✅ Activar 'Is Trigger' para detectar impactos
        }

        // ✅ Si hay un Rigidbody, asegurarse de que está en 'Is Kinematic'
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // ✅ Evita que la física lo mueva
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Impacto detectado con: " + other.gameObject.name); // ✅ Depuración

        if (other.CompareTag("Projectile")) // ✅ Si la bala impacta
        {
            vida--; // Reducimos vida
            Debug.Log("Enemy impactado, vida restante: " + vida);

            if (vida <= 0)
            {
                ApagarEnemigo();
            }

            // ✅ Desactivar la bala (si usas Pooling, la regresa al Pool)
            other.gameObject.SetActive(false);
        }
    }

    void ApagarEnemigo()
    {
        Debug.Log("❌ Enemy eliminado");

        // ✅ Instanciar el efecto WFX si está asignado
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
            Debug.Log("💥 WFX instanciado.");
        }
        else
        {
            Debug.LogError("⚠ No se encontró un efecto WFX en el enemigo.");
        }

        // ✅ Desactivar el enemigo después de un breve tiempo
        gameObject.SetActive(false);
    }
}
