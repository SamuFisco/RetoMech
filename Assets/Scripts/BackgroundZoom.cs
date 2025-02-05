using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackgroundZoom : MonoBehaviour
{
    public Image backgroundImage; // Imagen de fondo que har� zoom
    public GameObject[] buttons; // Botones a desactivar
    public GameObject centralImage; // Imagen central a desactivar
    public float moveDuration = 1.0f; // Duraci�n del zoom
    public float scaleAmount = 1.5f; // Tama�o final del zoom
    public string sceneName; // Nombre de la escena a cargar

    private Vector3 originalPosition;
    private Vector3 originalScale;

    void Start()
    {
        // Guarda la posici�n y escala inicial de la imagen
        originalPosition = backgroundImage.rectTransform.anchoredPosition;
        originalScale = backgroundImage.rectTransform.localScale;

        // Asegura que la imagen est� invisible al inicio
        backgroundImage.gameObject.SetActive(false);
    }

    public void PlayAnimation()
    {
        // Activa la imagen de fondo
        backgroundImage.gameObject.SetActive(true);

        // Desactiva los botones y la imagen central
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
        if (centralImage != null)
        {
            centralImage.SetActive(false);
        }

        // Mueve y escala la imagen de fondo
        LeanTween.move(backgroundImage.rectTransform, Vector3.zero, moveDuration).setEase(LeanTweenType.easeOutQuad);
        LeanTween.scale(backgroundImage.rectTransform, originalScale * scaleAmount, moveDuration).setEase(LeanTweenType.easeOutQuad);

        // Espera 2 segundos antes de cambiar de escena
        Invoke("ChangeScene", 2f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
