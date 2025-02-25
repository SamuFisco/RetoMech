using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject winCanvas; // Panel de Victoria
    public GameObject gameOverCanvas; // Panel de Game Over
    public string sceneGame = "GameScene"; // Nombre de la escena del juego
    public string sceneMenu = "ScenaMenu"; // Nombre de la escena del menú principal

    private void Start()
    {
        // Asegurar que ambos paneles inicien desactivados
        winCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);
    }

    // ✅ Método para mostrar la pantalla de victoria
    public void ShowWinScreen()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    // ✅ Método para mostrar la pantalla de Game Over
    public void ShowGameOverScreen()
    {
        gameOverCanvas.SetActive(true);
        Time.timeScale = 0f; // Pausar el juego
    }

    // ✅ Reiniciar el juego sin pasar por el menú
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Asegurar que el tiempo está activo
        SceneManager.LoadScene(sceneGame);
    }

    // ✅ Volver al menú principal
    public void VolverAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneMenu);
    }
}

