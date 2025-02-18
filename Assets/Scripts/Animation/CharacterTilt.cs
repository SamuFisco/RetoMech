using UnityEngine;

public class InclinacionPersonaje : MonoBehaviour
{
    [Header("Configuración de Inclinación")]
    public float inclinacionFrontalMaxima = 10f; // Inclinación máxima adelante/atrás
    public float inclinacionLateralMaxima = 10f; // Inclinación máxima a los lados
    public float inclinacionIdle = 3f; // Oscilación en Idle (cuando está quieto)
    public float velocidadInclinacion = 5f; // Velocidad de ajuste de la inclinación
    public float umbralMovimiento = 0.1f; // Sensibilidad mínima para detectar movimiento

    private CharacterController _controlador;
    private float velocidadAnterior = 0f; // Para detectar aceleración y frenado
    private float direccionAnterior = 0f; // Para detectar cambios de dirección

    void Start()
    {
        _controlador = GetComponent<CharacterController>();

        if (_controlador == null)
        {
            Debug.LogError("No se encontró CharacterController en " + gameObject.name);
        }
    }

    void Update()
    {
        AplicarInclinacion();
    }

    /// <summary>
    /// Aplica inclinaciones dinámicas según el movimiento del personaje.
    /// </summary>
    private void AplicarInclinacion()
    {
        Vector3 velocidad = _controlador.velocity;
        float velocidadActual = new Vector3(velocidad.x, 0, velocidad.z).magnitude;
        float diferenciaVelocidad = velocidadActual - velocidadAnterior; // Detectar aceleración o frenado
        float direccionActual = Mathf.Atan2(velocidad.x, velocidad.z) * Mathf.Rad2Deg; // Dirección del movimiento

        float inclinacionFrontalObjetivo = 0f;
        float inclinacionLateralObjetivo = 0f;

        // 🚀 Inclinación adelante/atrás según aceleración o frenado
        if (velocidadActual > umbralMovimiento)
        {
            if (diferenciaVelocidad > 0.05f) // Acelerando -> Inclinar hacia atrás
            {
                inclinacionFrontalObjetivo = -inclinacionFrontalMaxima;
            }
            else if (diferenciaVelocidad < -0.05f) // Frenando -> Inclinar hacia adelante
            {
                inclinacionFrontalObjetivo = inclinacionFrontalMaxima;
            }
        }

        // 🔄 Inclinación lateral en movimiento (al girar)
        float nuevaDireccion = Mathf.Sign(velocidad.x); // Detecta si se mueve a la derecha o izquierda
        if (velocidadActual > umbralMovimiento)
        {
            if (nuevaDireccion != direccionAnterior && Mathf.Abs(velocidad.x) > 0.05f)
            {
                inclinacionLateralObjetivo = -Mathf.Clamp(velocidad.x * inclinacionLateralMaxima, -inclinacionLateralMaxima, inclinacionLateralMaxima);
            }
        }
        else // 🔄 Oscilación en Idle
        {
            inclinacionLateralObjetivo = Mathf.Sin(Time.time * 2) * inclinacionIdle;
        }

        // 🎯 Aplicar interpolación suave
        float inclinacionFrontalSuave = Mathf.LerpAngle(transform.eulerAngles.x, inclinacionFrontalObjetivo, Time.deltaTime * velocidadInclinacion);
        float inclinacionLateralSuave = Mathf.LerpAngle(transform.eulerAngles.z, inclinacionLateralObjetivo, Time.deltaTime * velocidadInclinacion);
        transform.rotation = Quaternion.Euler(inclinacionFrontalSuave, transform.eulerAngles.y, inclinacionLateralSuave);

        velocidadAnterior = velocidadActual; // Actualizar velocidad anterior
        direccionAnterior = nuevaDireccion; // Actualizar dirección anterior
    }
}
