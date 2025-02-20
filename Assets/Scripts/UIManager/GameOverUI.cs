using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public string menuSceneName = "Menu"; // Nombre de la escena del men�

    // M�todo para reiniciar la escena actual desde el inicio
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Restaurar el tiempo de juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }

    // M�todo para ir al men� principal
    public void IrAlMenu()
    {
        Time.timeScale = 1; // Restaurar el tiempo en caso de que est� pausado
        SceneManager.LoadScene(menuSceneName); // Cargar la escena del men� principal
    }
}
