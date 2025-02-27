using UnityEngine;

/// <summary>
/// Este script gestiona la funcionalidad del power-up.
/// </summary>
public class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que colisiona es el jugador.
        if (other.CompareTag("Player"))
        {
            // Obtiene el componente PlayerEnergy (debe estar en el jugador).
            PlayerEnergy playerEnergy3 = other.GetComponent<PlayerEnergy>();

            if (playerEnergy3 != null)
            {
                // Restaura la energía al 100%.
                playerEnergy3.RestoreFullEnergy();
            }

            // Destruye el power-up tras ser recogido.
            Destroy(gameObject);
        }
    }
}
