using UnityEngine;

public class Controles : MonoBehaviour
{
    public RectTransform panel; // 🎮 Panel de controles en la UI
    public Vector2 posicionInicial; // 📌 Posición fuera de la pantalla
    public Vector2 posicionFinal; // 📌 Posición visible
    public float duracionAnimacion = 0.5f; // ⏳ Tiempo de la animación

    private bool estaVisible = false; // ✅ Estado del panel


    private void Awake()
    {
        // Asegurar que el panel inicie en la posición oculta
        panel.anchoredPosition = posicionInicial;
    }
    // 🔥 Método para mostrar el panel de controles
    public void MostrarControles()
    {
        if (!estaVisible)
        {
            estaVisible = true;
            LeanTween.move(panel, posicionFinal, duracionAnimacion)
                     .setEase(LeanTweenType.easeOutBack);
        }
    }

    // 🔥 Método para ocultar el panel de controles
    public void OcultarControles()
    {
        if (estaVisible)
        {
            estaVisible = false;
            LeanTween.move(panel, posicionInicial, duracionAnimacion)
                     .setEase(LeanTweenType.easeInBack);
        }
    }
}
