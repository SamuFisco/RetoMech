using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string menuSceneName = "Menu"; // Nombre de la escena del menú

    // Método para reiniciar la escena actual desde el inicio
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Restaurar el tiempo de juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }

    // Método para ir al menú principal
    public void IrAlMenu()
    {
        Time.timeScale = 1; // Restaurar el tiempo en caso de que esté pausado
        SceneManager.LoadScene(menuSceneName); // Cargar la escena del menú principal
    }
}
