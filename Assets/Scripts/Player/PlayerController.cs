using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 1.0f;
    [SerializeField] private float runSpeed = 2.0f;
    private bool _running;
    private InputAction _movementAction;
    private InputAction _runAction;
    private Vector2 _movementDirection;

    [Header("Camera Settings")]
    [SerializeField] private float cameraSensitivity = 2.0f;
    [SerializeField] private float clampRange = 90.0f;
    private Camera _playerCamera;
    private HeadBob _headBob;
    private Vector2 _mouseRotation;
    private InputAction _lookAction;
    private float cameraVerticalRotation;
    [Header("Footstep Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] footstepSounds;
    private float _stepInterval;
    private float _stepTime;
    private float _walkingStepInterval = .8f;
    private float _runningStepInterval = .5f;

    [Header("Settings")]
    [SerializeField] private InputActionAsset playerControls;

    private CharacterController _characterController;
    private float _currentSpeed;
    private bool _movementLocked;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _movementAction = playerControls.FindAction("Move");
        _lookAction = playerControls.FindAction("Look");
        _runAction = playerControls.FindAction("Run");

        _playerCamera = Camera.main;
        _headBob = GetComponentInChildren<HeadBob>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        GameEvents.OnReadNote += LockMovement;
        GameEvents.OnNoteClosed += UnlockMovement;

        GameEvents.OnHouseInvaded += LockMovement;
    }

    void OnDisable()
    {
        GameEvents.OnReadNote -= LockMovement;
        GameEvents.OnNoteClosed -= UnlockMovement;

        GameEvents.OnHouseInvaded -= LockMovement;
    }

    void Update()
    {
        if(_movementLocked) return;

        _movementDirection = _movementAction.ReadValue<Vector2>();
        _mouseRotation = _lookAction.ReadValue<Vector2>();

        _running = _runAction.ReadValue<float>() > 0.5f;

        if(_running) {
            _stepInterval = _runningStepInterval;
        } else if(!_running && _stepInterval != _walkingStepInterval) {
            _stepInterval = _walkingStepInterval;
        }

        if(_characterController.isGrounded && _characterController.velocity.magnitude >= .1f) {
            _stepTime += Time.deltaTime;
            if(_stepTime >= _stepInterval) {
                PlayFootstep();
                _stepTime = 0f;
            }
        } else {
            _stepTime = 0f;
        }
    }

    void FixedUpdate()
    {
        if(_movementLocked) return;

        _currentSpeed = _running ? runSpeed : walkSpeed;

        Vector3 movement = new Vector3(_movementDirection.x, 0, _movementDirection.y).normalized;
        movement = transform.rotation * movement;

        _characterController.SimpleMove(movement * _currentSpeed);
        _headBob.ApplyHeadBob(movement.magnitude * _currentSpeed);
    }

    void LateUpdate()
    {
        if(_movementLocked) return;

        float mouseXRotation = _mouseRotation.x * cameraSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        cameraVerticalRotation -= _mouseRotation.y * cameraSensitivity;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -clampRange, clampRange);
        _playerCamera.transform.localRotation = Quaternion.Euler(cameraVerticalRotation, 0, 0);
    }

    private void PlayFootstep() {
        AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];

        audioSource.pitch = Random.Range(0.8f, 1f);
        audioSource.PlayOneShot(clip);
    }

    private void LockMovement(string text) {
        _movementLocked = true;
    }

    private void LockMovement() {
        _movementLocked = true;
    }

    private void UnlockMovement() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _movementLocked = false;
    }
}
