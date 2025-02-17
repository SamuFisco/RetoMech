using UnityEngine;

public class CharacterIdleRotation : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    public float rotationAmount = 5f; // Grados de rotación en idle (izquierda-derecha)
    public float rotationSpeed = 2f; // Velocidad de la rotación en idle
    public float movementThreshold = 0.1f; // Mínima velocidad para detectar movimiento
    public float smoothness = 5f; // Suavidad de la transición entre idle y movimiento

    private CharacterController _controller;
    private float idleRotationY = 0f;

    void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (_controller == null)
        {
            Debug.LogError("No se encontró CharacterController en " + gameObject.name);
        }
    }

    void Update()
    {
        ApplyIdleRotation();
    }

    /// <summary>
    /// Aplica una rotación suave en Idle y la detiene cuando el personaje se mueve.
    /// </summary>
    private void ApplyIdleRotation()
    {
        // Obtener la velocidad en los ejes X y Z
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;

        if (speed > movementThreshold)
        {
            // Si el personaje se mueve, detener la rotación en Idle
            idleRotationY = 0f;
        }
        else
        {
            // Si el personaje está en Idle, rotarlo suavemente de un lado a otro
            idleRotationY = Mathf.Sin(Time.time * rotationSpeed) * rotationAmount;
        }

        // Aplicar suavemente la rotación
        float smoothRotationY = Mathf.LerpAngle(transform.eulerAngles.y, transform.eulerAngles.y + idleRotationY, Time.deltaTime * smoothness);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, smoothRotationY, transform.eulerAngles.z);
    }
}
