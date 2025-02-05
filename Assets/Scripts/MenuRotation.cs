using UnityEngine;
using UnityEngine.EventSystems;

public class MenuRotationUI : MonoBehaviour
{
    public RectTransform centralImage; // Imagen que rotará
    public float rotationTime = 0.5f;  // Duración de la rotación
    public float rotationAmount = 10f; // Ángulo de inclinación

    private Vector3 originalRotation; // Guarda la rotación inicial

    void Start()
    {
        originalRotation = centralImage.eulerAngles; // Guarda la rotación original
    }

    public void RotateToRight()
    {
        Debug.Log("Rotando a la derecha"); // Verifica si la función se ejecuta
        LeanTween.rotateZ(centralImage.gameObject, originalRotation.z + rotationAmount, rotationTime).setEase(LeanTweenType.easeOutQuad);
    }

    public void RotateToLeft()
    {
        Debug.Log("Rotando a la izquierda"); // Verifica si la función se ejecuta
        LeanTween.rotateZ(centralImage.gameObject, originalRotation.z - rotationAmount, rotationTime).setEase(LeanTweenType.easeOutQuad);
    }

    public void ResetRotation()
    {
        Debug.Log("Restableciendo rotación"); // Verifica si la función se ejecuta
        LeanTween.rotateZ(centralImage.gameObject, originalRotation.z, rotationTime).setEase(LeanTweenType.easeOutQuad);
    }
}
