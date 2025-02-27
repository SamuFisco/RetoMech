
using UnityEngine;


public class PlayerEnergy3 : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f; // Energía máxima del jugador.
    private float currentEnergy; // Energía actual.

    private void Start()
    {
        // Inicializa la energía al máximo al comenzar.
        currentEnergy = maxEnergy;
    }

    
    public void RestoreFullEnergy()
    {
        currentEnergy = maxEnergy;
        Debug.Log("¡Energía restaurada al 100%! Puedes disparar sin esperar.");
    }

   
    public void UseEnergy(float amount)
    {
        currentEnergy -= amount;
        if (currentEnergy < 0) currentEnergy = 0;
        // Destruye el power-up tras ser recogido.
        Destroy(gameObject);
    }

    public bool CanShoot()
    {
        return currentEnergy > 0;

    }
}