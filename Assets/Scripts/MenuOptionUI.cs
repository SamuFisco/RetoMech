using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsUI : MonoBehaviour
{
    public RectTransform optionsMenu; // Panel del men� lateral
    public GameObject centralImage; // Imagen central
    public GameObject[] buttons; // Botones principales del men�

    public float moveDuration = 0.5f; // Duraci�n de la animaci�n

    private Vector2 initialPosition = new Vector2(-1275, -75); // Posici�n inicial
    private Vector2 openPosition = new Vector2(-6, -75); // Posici�n abierta

    void Start()
    {
        // Asegura que el men� empiece en la posici�n inicial
        optionsMenu.anchoredPosition = initialPosition;
    }

    public void OpenMenu()
    {
        // Mueve el men� a la posici�n abierta
        LeanTween.move(optionsMenu, openPosition, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // Desactiva los botones y la imagen central
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        centralImage.SetActive(false);
    }

    public void CloseMenu()
    {
        // Mueve el men� de vuelta a su posici�n inicial
        LeanTween.move(optionsMenu, initialPosition, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // Reactiva los botones y la imagen central
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(true);
        }
        centralImage.SetActive(true);
    }
}
