using UnityEngine;
using System.Collections;

public class PilotIdle : MonoBehaviour
{
    [Header("Configuración del Idle del Piloto")]
    public float jumpHeight = 0.2f; // Altura del salto en Idle
    public float jumpSpeed = 2f; // Velocidad del salto
    public float idleDelay = 2f; // Tiempo de espera antes de cada salto

    [Header("Detección de Movimiento")]
    public CharacterController characterController;
    public float movementThreshold = 0.1f; // Mínima velocidad para detectar movimiento

    private Vector3 originalPosition;
    private bool isIdle = false;
    private bool isJumping = false;

    void Start()
    {
        originalPosition = transform.localPosition;

        if (characterController == null)
        {
            Debug.LogError("Falta asignar el CharacterController en " + gameObject.name);
        }
    }

    void Update()
    {
        CheckIdleState();
    }

    /// <summary>
    /// Verifica si el personaje está en Idle y activa el saltito cada 2 segundos.
    /// </summary>
    private void CheckIdleState()
    {
        float speed = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;

        if (speed < movementThreshold)
        {
            if (!isIdle)
            {
                isIdle = true;
                StartCoroutine(IdleJumpRoutine()); // Iniciar la animación cada 2 segundos
            }
        }
        else
        {
            isIdle = false;
            isJumping = false;
            StopAllCoroutines(); // Detener el salto si el personaje se mueve
            transform.localPosition = originalPosition; // Resetear posición
        }
    }

    /// <summary>
    /// Corrutina que hace que el piloto salte cada 2 segundos.
    /// </summary>
    private IEnumerator IdleJumpRoutine()
    {
        while (isIdle)
        {
            if (!isJumping)
            {
                isJumping = true;
                yield return JumpUp(); // Subir
                yield return JumpDown(); // Bajar
                yield return new WaitForSeconds(idleDelay); // Esperar 2 segundos
                isJumping = false;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Hace que el piloto suba suavemente.
    /// </summary>
    private IEnumerator JumpUp()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = startPos + new Vector3(0, jumpHeight, 0);

        while (elapsedTime < 1f / jumpSpeed)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime * jumpSpeed);
            yield return null;
        }
    }

    /// <summary>
    /// Hace que el piloto baje suavemente.
    /// </summary>
    private IEnumerator JumpDown()
    {
        float elapsedTime = 0f;
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = originalPosition;

        while (elapsedTime < 1f / jumpSpeed)
        {
            elapsedTime += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(startPos, targetPos, elapsedTime * jumpSpeed);
            yield return null;
        }
    }
}
