using UnityEngine;
using System.Collections;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float maxEnergy = 100f;
    private float currentEnergy;
    private bool infiniteEnergyActive = false;

    private void Start()
    {
        currentEnergy = maxEnergy;
    }

    public void ConsumeEnergy(float amount)
    {
        if (!infiniteEnergyActive)
        {
            currentEnergy = Mathf.Max(currentEnergy - amount, 0);
        }
    }

    public void RestoreFullEnergy()
    {
        currentEnergy = maxEnergy;
    }

    public void ActivateInfiniteEnergy(float duration)
    {
        StartCoroutine(InfiniteEnergyCoroutine(duration));
    }

    private IEnumerator InfiniteEnergyCoroutine(float duration)
    {
        infiniteEnergyActive = true;
        yield return new WaitForSeconds(duration);
        infiniteEnergyActive = false;
    }
}
