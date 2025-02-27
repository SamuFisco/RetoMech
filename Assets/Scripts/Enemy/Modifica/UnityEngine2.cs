using UnityEngine;
using System.Collections;

public class PlayerEnergy2 : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f;
    private float currentEnergy;
    private bool infiniteEnergyActive = false;

    private void Start()
    {
        currentEnergy = maxEnergy;
    }

    public void RestoreEnergy()
    {
        currentEnergy = maxEnergy;
        Debug.Log("Energía restaurada al 100%");
    }

    public void ActivateInfiniteEnergy(float duration)
    {
        if (!infiniteEnergyActive)
        {
            StartCoroutine(InfiniteEnergyRoutine(duration));
        }
    }

    private IEnumerator InfiniteEnergyRoutine(float duration)
    {
        infiniteEnergyActive = true;
        Debug.Log("Energía infinita activada por " + duration + " segundos");
        yield return new WaitForSeconds(duration);
        infiniteEnergyActive = false;
        Debug.Log("Energía infinita desactivada");
    }

    public bool CanShoot()
    {
        return infiniteEnergyActive || currentEnergy > 0;
    }

    public void ConsumeEnergy(float amount)
    {
        if (!infiniteEnergyActive)
        {
            currentEnergy -= amount;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }
    }
}
