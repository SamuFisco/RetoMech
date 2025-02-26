using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public GameObject target; // El objeto a sacudir (Debe ser la Cámara)
    public float intensity = 1f; // Intensidad del sacudido
    public float duration = 0.5f; // Duración total

    private Vector3 originalPosition;

    private void Start()
    {
        if (target == null)
        {
            target = Camera.main.gameObject; // Asigna la cámara automáticamente si no está definida
        }

        originalPosition = target.transform.position;
    }

    public void ShakePosition()
    {
        if (target == null) return;

        LeanTween.cancel(target); // Cancela cualquier animación previa para evitar acumulación

        LeanTween.value(gameObject, 0f, intensity, duration)
            .setOnUpdate((float value) => {
                // Generar desplazamiento aleatorio en los ejes X e Y
                Vector3 randomOffset = new Vector3(
                    Random.Range(-value, value),
                    Random.Range(-value, value),
                    0f
                );
                target.transform.position = originalPosition + randomOffset;
            })
            .setEase(LeanTweenType.easeShake)
            .setOnComplete(() => {
                // Restaurar la posición original
                target.transform.position = originalPosition;
            });
    }
}
