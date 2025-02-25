using UnityEngine;
using UnityEngine.UI;

public class MenuOptionsUI : MonoBehaviour
{
    public RectTransform optionsMenu; // 📌 Panel del menú lateral
    public GameObject centralImage; // 🎮 Imagen central del menú
    public GameObject[] buttons; // 🎮 Botones principales del menú
    public Button exitButton; // 🔥 Botón de salir

    public float moveDuration = 0.5f; // ⏳ Duración de la animación

    private Vector2 initialPosition = new Vector2(-1275, -75); // 📌 Posición inicial oculta
    private Vector2 openPosition = new Vector2(-6, -75); // 📌 Posición abierta

    void Start()
    {
        // Asegura que el menú empiece oculto
        optionsMenu.anchoredPosition = initialPosition;

        // Asigna el evento al botón de salir
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(SalirDelJuego);
        }
    }

    public void OpenMenu()
    {
        // 🔥 Mueve el menú a la posición abierta con animación suave
        LeanTween.move(optionsMenu, openPosition, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // 🔥 Desactiva los botones y la imagen central
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        centralImage.SetActive(false);
    }

    public void CloseMenu()
    {
        // 🔥 Mueve el menú a la posición inicial con animación
        LeanTween.move(optionsMenu, initialPosition, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // 🔥 Reactiva los botones y la imagen central
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(true);
        }
        centralImage.SetActive(true);
    }

    public void SalirDelJuego()
    {
        Debug.Log("🚪 Saliendo del juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // ❌ Cierra el juego en el Editor de Unity
#else
        Application.Quit(); // ❌ Cierra la aplicación en la Build
#endif
    }
}
