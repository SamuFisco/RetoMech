using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TimerManager : MonoBehaviour
{
    public float timeRemaining = 420f; // 7 minutos en segundos
    public TextMeshProUGUI timerText; // Texto de UI para mostrar el tiempo
    public TextMeshProUGUI scoreText; // Texto de UI para la puntuación
    public TextMeshProUGUI enemyText; // Texto de UI para la cantidad de enemigos restantes
    public int playerScore = 0; // Puntaje del jugador
    private bool isGameOver = false; // Evitar múltiples llamadas a GameOver
    private int enemyCount; // Número de enemigos restantes en la escena

    void Start()
    {
        // Contar los enemigos en la escena al inicio
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateEnemyUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime; // Reducir el tiempo cada frame
                UpdateTimerUI();
            }
            else
            {
                GameOver(); // Finaliza el juego si el tiempo se acaba
            }
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Puntos: " + playerScore;
    }

    // Método para actualizar la UI del contador de enemigos
    void UpdateEnemyUI()
    {
        enemyText.text = "Enemigos restantes: " + enemyCount;
    }

    // Método llamado cuando un enemigo es derrotado
    public void EnemyDefeated()
    {
        enemyCount--; // Reduce la cantidad de enemigos
        UpdateEnemyUI();

        if (enemyCount <= 0) // Si no quedan enemigos, el jugador gana
        {
            WinGame();
        }
    }

    void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Tiempo agotado. Fin del juego.");
            SceneManager.LoadScene("GameOver"); // Cargar pantalla de Game Over
        }
    }

    void WinGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("¡Has derrotado a todos los enemigos! Victoria.");
            CalculateFinalScore();
            SceneManager.LoadScene("WinScene"); // Cargar pantalla de victoria
        }
    }

    public void CalculateFinalScore()
    {
        int bonusTimePoints = Mathf.FloorToInt(timeRemaining) * 10;
        playerScore += bonusTimePoints;
        UpdateScoreUI();
        Debug.Log("Puntos finales: " + playerScore);
    }
}
