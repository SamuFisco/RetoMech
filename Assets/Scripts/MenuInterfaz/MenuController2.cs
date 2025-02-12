using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController2 : MonoBehaviour
{
    public RectTransform menu; // Referencia al Menú (UI)
    public EventSystem eventSystem; // Referencia al EventSystem

    private bool menuActivo = false;
    private float posicionInicialY = 1120f;
    private float posicionFinalY = -62f;
    private float duracionMovimiento = 0.5f;

    void Start()
    {
        menu.anchoredPosition = new Vector2(menu.anchoredPosition.x, posicionInicialY);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AlternarMenu();
        }
    }

    public void AlternarMenu()
    {
        menuActivo = !menuActivo;

        float destinoY = menuActivo ? posicionFinalY : posicionInicialY;
        LeanTween.moveY(menu, destinoY, duracionMovimiento).setEase(LeanTweenType.easeOutQuad).setIgnoreTimeScale(true);

        Time.timeScale = menuActivo ? 0 : 1;

        // Activar el cursor y desbloquearlo cuando el menú está activo
        Cursor.visible = menuActivo;
        Cursor.lockState = menuActivo ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void CerrarMenu()
    {
        if (menuActivo)
        {
            menuActivo = false;
            LeanTween.moveY(menu, posicionInicialY, duracionMovimiento).setEase(LeanTweenType.easeOutQuad);
            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SalirDelJuego()
    {
        print("Exit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Cierra el juego en el Editor de Unity
#else
            Application.Quit(); // Cierra la aplicación en la Build
#endif
    }
    public void ScenaMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ScenaMenu");

    }

}
