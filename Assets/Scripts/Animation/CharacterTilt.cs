using UnityEngine;

public class CharacterTilt : MonoBehaviour
{
    [Header("Configuración de Inclinación")]
    public float inclinacionFrontalMaxima = 10f; // Inclinación máxima adelante/atrás
    public float inclinacionLateralMaxima = 10f; // Inclinación máxima a los lados
    public float inclinacionIdle = 3f; // Oscilación en Idle
    public float velocidadInclinacion = 5f; // Velocidad de ajuste de la inclinación
    public float umbralMovimiento = 0.1f; // Sensibilidad mínima para detectar movimiento

    private CharacterController _controlador;
    private float velocidadAnterior = 0f; // Para detectar aceleración y frenado

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
        float diferenciaVelocidad = velocidadActual - velocidadAnterior;
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

        // 🔄 Inclinación lateral cuando el jugador gira Y se mueve
        float inputHorizontal = Input.GetAxis("Horizontal");
        if (velocidadActual > umbralMovimiento) // Solo inclinar si se está moviendo
        {
            if (Mathf.Abs(inputHorizontal) > 0.1f) // Detectar si se está girando
            {
                inclinacionLateralObjetivo = -inputHorizontal * inclinacionLateralMaxima;
            }
        }
        else // 🔄 Oscilación en Idle cuando está quieto
        {
            inclinacionLateralObjetivo = Mathf.Sin(Time.time * 2) * inclinacionIdle;
        }

        // 🎯 Aplicar interpolación suave
        float inclinacionFrontalSuave = Mathf.LerpAngle(transform.eulerAngles.x, inclinacionFrontalObjetivo, Time.deltaTime * velocidadInclinacion);
        float inclinacionLateralSuave = Mathf.LerpAngle(transform.eulerAngles.z, inclinacionLateralObjetivo, Time.deltaTime * velocidadInclinacion);
        transform.rotation = Quaternion.Euler(inclinacionFrontalSuave, transform.eulerAngles.y, inclinacionLateralSuave);

        velocidadAnterior = velocidadActual; // Actualizar velocidad anterior
    }
}

