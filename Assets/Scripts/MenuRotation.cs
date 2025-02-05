using UnityEngine;
using UnityEngine.EventSystems;

public class MenuRotationUI : MonoBehaviour
{
    public RectTransform centralImage; // Imagen que rotar�
    public float rotationTime = 0.5f;  // Duraci�n de la rotaci�n
    public float rotationAmount = 10f; // �ngulo de inclinaci�n

    private Vector3 originalRotation; // Guarda la rotaci�n inicial

    void Start()
    {
        originalRotation = centralImage.eulerAngles; // Guarda la rotaci�n original
    }

    public void RotateToRight()
    {
        Debug.Log("Rotando a la derecha"); // Verifica si la funci�n se ejecuta
        LeanTween.rotateZ(centralImage.gameObject, originalRotation.z + rotationAmount, rotationTime).setEase(LeanTweenType.easeOutQuad);
    }

    public void RotateToLeft()
    {
        Debug.Log("Rotando a la izquierda"); // Verifica si la funci�n se ejecuta
        LeanTween.rotateZ(centralImage.gameObject, originalRotation.z - rotationAmount, rotationTime).setEase(LeanTweenType.easeOutQuad);
    }

    public void ResetRotation()
    {
        Debug.Log("Restableciendo rotaci�n"); // Verifica si la funci�n se ejecuta
        LeanTween.rotateZ(centralImage.gameObject, originalRotation.z, rotationTime).setEase(LeanTweenType.easeOutQuad);
    }
}
