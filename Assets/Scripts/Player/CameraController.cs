using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Referencia al objeto del jugador
    private Vector3 offset; // Distancia entre la cámara y el jugador

    void Start()
    {
        // Calcular la diferencia entre la posición de la cámara y la del jugador
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Mantener la misma distancia con el jugador
        transform.position = player.transform.position + offset;
    }
}
