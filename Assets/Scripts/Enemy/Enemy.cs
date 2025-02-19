using UnityEngine;

public class Enemy : MonoBehaviour
{
    public TimerManager timerManager;

    void Start()
    {
        timerManager = FindObjectOfType<TimerManager>();
    }

    public void Die()
    {
        timerManager.AddScore(2); // Sumar puntos al eliminar enemigo
        Destroy(gameObject);
    }
}
