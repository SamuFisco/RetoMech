using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveRueda : MonoBehaviour
{
    [Header("Player")]
    [Tooltip("Velocidad de movimiento del personaje en m/s")]
    public float MoveSpeed = 2.0f;

    [Tooltip("Velocidad de sprint del personaje en m/s")]
    public float SprintSpeed = 60f;

    [Tooltip("Cuán rápido gira el personaje para alinear su dirección de movimiento")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;

    [Tooltip("Aceleración y desaceleración")]
    public float SpeedChangeRate = 10.0f;

    [Header("Gravity & Jump")]
    public float Gravity = -15.0f;
    public float JumpHeight = 1.2f;
    public float JumpTimeout = 0.50f;
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;

    [Header("Cinemachine")]
    public GameObject CinemachineCameraTarget;
    public float TopClamp = 70.0f;
    public float BottomClamp = -30.0f;
    public float CameraAngleOverride = 0.0f;
    public bool LockCameraPosition = false;

    [Header("Animación rueda e inclinación")]
    [SerializeField] Transform rueda;
    [SerializeField] float rotacion = 5.0f;
    [SerializeField] float maxTiltAngle = 15f; // Ángulo máximo de inclinación
    [SerializeField] float tiltSmoothness = 5f; // Suavidad de la inclinación

    // Variables internas
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private float _speed;
    private float _animationBlend;
    private float _targetRotation = 0.0f;
    private float _rotationVelocity;
    private float _verticalVelocity;
    private float _terminalVelocity = 53.0f;
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    private PlayerInput _playerInput;
    private Animator _animator;
    private CharacterController _controller;
    private StarterAssetsInputs _input;
    private GameObject _mainCamera;

    private const float _threshold = 0.01f;

    private bool IsCurrentDeviceMouse => _playerInput.currentControlScheme == "KeyboardMouse";

    private void Awake()
    {
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    private void Start()
    {
        _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        AnimRueda();
        Move();      // Se aplica la lógica de velocidad (incluyendo sprint)
        FallOut();
        ApplyTilt();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    /// <summary>
    /// Verifica si el personaje está en el suelo.
    /// </summary>
    private void GroundedCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
    }

    /// <summary>
    /// Aplica la rotación de la cámara según el input del jugador.
    /// </summary>
    private void CameraRotation()
    {
        if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
        {
            float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
            _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
        }

        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

        CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
    }

    /// <summary>
    /// Aplica la inclinación del robot según la velocidad de movimiento.
    /// </summary>
    private void ApplyTilt()
    {
        Vector3 velocity = new Vector3(_controller.velocity.x, 0, _controller.velocity.z);
        float speed = velocity.magnitude;
        float direction = Mathf.Sign(velocity.x);
        float targetTilt = maxTiltAngle * direction * (speed / SprintSpeed);
        float smoothTilt = Mathf.LerpAngle(transform.eulerAngles.z, targetTilt, Time.deltaTime * tiltSmoothness);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, smoothTilt);
    }

    /// <summary>
    /// Hace que la rueda gire en base al movimiento del personaje.
    /// </summary>
    private void AnimRueda()
    {
        if (rueda != null)
        {
            float rotationAmount = _controller.velocity.magnitude * rotacion * Time.deltaTime;
            rueda.Rotate(Vector3.right, rotationAmount);
        }
    }

    /// <summary>
    /// Controla el movimiento del personaje aplicando la lógica de velocidad:
    /// - Selecciona la velocidad objetivo (normal o sprint).
    /// - Interpola suavemente entre la velocidad actual y la velocidad objetivo.
    /// - Aplica el movimiento y la rotación al CharacterController.
    /// </summary>
    private void Move()
    {
        // Selecciona la velocidad objetivo: SprintSpeed si se activa el sprint, o MoveSpeed en condiciones normales.
        float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
        // Si no hay input de movimiento y además no se está presionando sprint, se establece targetSpeed a 0.
        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        // Calcula la velocidad horizontal actual (descartando la componente vertical).
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;
        float speedOffset = 0.1f; // margen para evitar cambios bruscos
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        // Interpolación suave entre la velocidad actual y la velocidad objetivo.
        if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);
            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        // Actualiza el blend de animación según la velocidad.
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        // Calcula la dirección de movimiento a partir del input.
        Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

        // Si hay input de movimiento, calcula la rotación objetivo.
        if (_input.move != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }

        // Aplica el movimiento al CharacterController.
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    /// <summary>
    /// Aplica la gravedad incrementando la velocidad vertical hasta alcanzar un límite terminal.
    /// </summary>
    private void FallOut()
    {
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    /// <summary>
    /// Restringe un ángulo entre un mínimo y un máximo.
    /// </summary>
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
