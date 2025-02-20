using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para cambiar escenas

public class GameOverUI : MonoBehaviour
{
    public EnemyVision enemyVision;
    public string menuSceneName = "Menu"; // Nombre de la escena del Men�

    // M�todo para reiniciar la escena actual desde el inicio
    public void ReiniciarJuego()
    {
        Time.timeScale = 1; // Asegurar que el juego no est� pausado
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Recarga la escena actual
    }

    // M�todo para ir al men� principal
    public void IrAlMenu()
    {
        Time.timeScale = 1; // Asegurar que el tiempo se restaure
        SceneManager.LoadScene(menuSceneName); // Cargar la escena del Men�
    }

    // M�todo para activar la bandera de reinicio en EnemyVision
    public void ActivarReinicioDesdeUI()
    {
        if (enemyVision != null)
        {
            enemyVision.ActivarReinicio();
        }
    }
}
