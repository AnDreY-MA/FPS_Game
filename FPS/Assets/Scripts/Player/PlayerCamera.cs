using UnityEngine;

public class PlayerCamera : Player
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _mouseSensivity = 4f;
    [SerializeField] private float _mouseSmothTime = 0.03f;


    private float _cameraPitch = 0;

    private Vector2 _currentMouseDelta = Vector2.zero;
    private Vector2 _currentMouseDeltaVelocity = Vector2.zero;

    private void Start()
    {
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
    }

    private void Update()
    {
        MouseLook();
    }

    private void MouseLook()
    {
        Vector2 targetMouseDelta = _playerInput.Player.Look.ReadValue<Vector2>();

        _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, _mouseSmothTime);

        _cameraPitch -= _currentMouseDelta.y * _mouseSensivity;
        _cameraPitch = Mathf.Clamp(_cameraPitch, -90, 90);

        _camera.localEulerAngles = Vector3.right * _cameraPitch;

        transform.Rotate(Vector3.up * _currentMouseDelta.x * _mouseSensivity);
    }
}
