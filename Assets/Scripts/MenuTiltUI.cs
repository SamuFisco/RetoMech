using UnityEngine;
using UnityEngine.EventSystems;

public class MenuTiltUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform centralImage; // Imagen que se moverá
    public float moveAmount = 10f; // Distancia de movimiento en X e Y
    public float moveTime = 0.5f; // Duración de la animación
    public bool moveRight, moveLeft, moveUp, moveDown;

    public AudioSource audioSource; // Componente de audio
    public AudioClip tiltSound; // Sonido para la inclinación

    private Vector3 originalPosition; // Guarda la posición original

    void Start()
    {
        originalPosition = centralImage.anchoredPosition; // Almacena la posición original
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Cursor sobre: " + gameObject.name);

        // Reproducir sonido si está asignado
        if (audioSource != null && tiltSound != null)
        {
            audioSource.PlayOneShot(tiltSound);
        }

        if (moveRight)
            LeanTween.moveX(centralImage, originalPosition.x + moveAmount, moveTime).setEase(LeanTweenType.easeOutQuad);
        if (moveLeft)
            LeanTween.moveX(centralImage, originalPosition.x - moveAmount, moveTime).setEase(LeanTweenType.easeOutQuad);
        if (moveUp)
            LeanTween.moveY(centralImage, originalPosition.y + moveAmount, moveTime).setEase(LeanTweenType.easeOutQuad);
        if (moveDown)
            LeanTween.moveY(centralImage, originalPosition.y - moveAmount, moveTime).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Cursor salió de: " + gameObject.name);
        LeanTween.move(centralImage, originalPosition, moveTime).setEase(LeanTweenType.easeOutQuad);
    }
}
