using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string sceneGame = "GameScene"; // Nombre de la escena de juego

    // Método público para reiniciar la escena correctamente
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Asegurar que el tiempo esté activo
        SceneManager.LoadScene(sceneGame); // Cargar la escena de juego directamente
    }
}
