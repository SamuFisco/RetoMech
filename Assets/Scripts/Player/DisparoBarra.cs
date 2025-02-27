using UnityEngine;
using UnityEngine.UI;

public class DisparoBarra : MonoBehaviour
{
    [Header("Energía")]
    public float maxEnergy = 100f;  // Energía máxima
    private float currentEnergy;    // Energía actual
    public float energyCost = 33f;  // Energía consumida por disparo doble
    public float rechargeRate = 10f; // Velocidad de recarga por segundo
    public Slider energyBar;        // Referencia a la barra de energía UI

    [Header("Disparo")]
    public GameObject projectilePrefab;
    public Transform shootPoint1;
    public Transform shootPoint2;

    void Start()
    {
        currentEnergy = maxEnergy;
        UpdateEnergyUI();
    }

    void Update()
    {
        // Recarga progresiva de energía
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += rechargeRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            UpdateEnergyUI();
        }

        // Disparo con consumo de energía al hacer clic con el botón izquierdo del ratón
        if (Input.GetMouseButtonDown(0) && CanShoot())
        {
            Shoot();
        }
    }

    bool CanShoot()
    {
        return currentEnergy >= energyCost; // Verifica si hay suficiente energía para disparo doble
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint1.position, Quaternion.identity);
        Instantiate(projectilePrefab, shootPoint2.position, Quaternion.identity);

        // Restamos energía una única vez
        currentEnergy -= energyCost;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyUI();
    }

    void UpdateEnergyUI()
    {
        if (energyBar != null)
        {
            energyBar.value = currentEnergy / maxEnergy;
        }
    }
}
