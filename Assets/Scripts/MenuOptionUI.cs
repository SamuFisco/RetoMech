using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsUI : MonoBehaviour
{
    public RectTransform optionsMenu; // Panel del menú lateral
    public GameObject centralImage; // Imagen central
    public GameObject[] buttons; // Botones principales del menú

    public float moveDuration = 0.5f; // Duración de la animación

    private Vector2 initialPosition = new Vector2(-1275, -75); // Posición inicial
    private Vector2 openPosition = new Vector2(-6, -75); // Posición abierta

    void Start()
    {
        // Asegura que el menú empiece en la posición inicial
        optionsMenu.anchoredPosition = initialPosition;
    }

    public void OpenMenu()
    {
        // Mueve el menú a la posición abierta
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
        // Mueve el menú de vuelta a su posición inicial
        LeanTween.move(optionsMenu, initialPosition, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // Reactiva los botones y la imagen central
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(true);
        }
        centralImage.SetActive(true);
    }
}
