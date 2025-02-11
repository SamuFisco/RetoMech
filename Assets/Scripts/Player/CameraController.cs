using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Referencia al objeto del jugador
    private Vector3 offset; // Distancia entre la c�mara y el jugador

    void Start()
    {
        // Calcular la diferencia entre la posici�n de la c�mara y la del jugador
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Mantener la misma distancia con el jugador
        transform.position = player.transform.position + offset;
    }
}
