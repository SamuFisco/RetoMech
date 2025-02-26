using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public Transform rootVision; // Referencia al objeto RootVision dentro del jugador
    public float intensity = 1f; // Intensidad del efecto de sacudida
    public float duration = 0.5f; // Duración del efecto

    private Vector3 originalLocalPosition; // Almacena la posición inicial del objeto

    private void Start()
    {
        if (rootVision == null)
        {
            // Busca dinámicamente el objeto RootVision dentro del jugador
            rootVision = transform.Find("RootVision");

            // Si no se encuentra RootVision, muestra un mensaje de error y detiene la ejecución
            if (rootVision == null)
            {
                Debug.LogError("RootVision no encontrado como hijo del Player.");
                return;
            }
        }

        // Guarda la posición local original del objeto
        originalLocalPosition = rootVision.localPosition;
    }

    public void ShakePosition()
    {
        // Si rootVision no está asignado, se detiene la ejecución
        if (rootVision == null) return;

        // Cancela cualquier animación previa en el objeto para evitar acumulaciones
        LeanTween.cancel(rootVision.gameObject);

        // Aplica un efecto de sacudida moviendo la posición local con un desplazamiento aleatorio
        LeanTween.moveLocal(rootVision.gameObject, originalLocalPosition + (Vector3)Random.insideUnitCircle * intensity, duration)
            .setEase(LeanTweenType.punch) // Usa un tipo de animación de golpe para el efecto
            .setOnComplete(() => {
                // Al finalizar la animación, restaura la posición original del objeto
                rootVision.localPosition = originalLocalPosition;
            });
    }
}
