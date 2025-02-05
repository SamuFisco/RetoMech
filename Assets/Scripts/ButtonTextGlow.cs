using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonTextGlow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI buttonText; // Referencia al texto del botón
    public float fadeDuration = 0.5f; // Duración de la animación

    void Start()
    {
        // Inicia con el texto completamente apagado
        SetTextAlpha(0f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Cursor sobre el botón: " + gameObject.name);
        LeanTween.value(gameObject, UpdateAlpha, buttonText.color.a, 1f, fadeDuration).setEase(LeanTweenType.easeOutQuad);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Cursor salió del botón: " + gameObject.name);
        LeanTween.value(gameObject, UpdateAlpha, buttonText.color.a, 0f, fadeDuration).setEase(LeanTweenType.easeOutQuad);
    }

    void UpdateAlpha(float alpha)
    {
        Color color = buttonText.color;
        color.a = alpha;
        buttonText.color = color;
    }

    void SetTextAlpha(float alpha)
    {
        Color color = buttonText.color;
        color.a = alpha;
        buttonText.color = color;
    }
}
