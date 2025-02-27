using UnityEngine;

public class PowerUpEnergy : MonoBehaviour
{
    public enum PowerUpType { TodalaEnergia, EnergiaInfinita }
    public PowerUpType powerUpType;
    [SerializeField] private float DuracionEnergia = 30f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Comparando etiqueta del Player
        {
            PlayerEnergy playerEnergy = other.GetComponent<PlayerEnergy>();
            if (playerEnergy != null)
            {
                ApplyPowerUp(playerEnergy);
            }
            Destroy(gameObject);
        }
    }

    private void ApplyPowerUp(PlayerEnergy playerEnergy)
    {
        if (powerUpType == PowerUpType.TodalaEnergia)
        {
            playerEnergy.RestoreFullEnergy();
        }
        else
        {
            Debug.LogWarning("RestauraEnergia");
        }
        if (powerUpType == PowerUpType.EnergiaInfinita)
        {
            playerEnergy.ActivateInfiniteEnergy(DuracionEnergia);
        }
        else
        {
            Debug.LogWarning("Energia100");
        }
    }
}
