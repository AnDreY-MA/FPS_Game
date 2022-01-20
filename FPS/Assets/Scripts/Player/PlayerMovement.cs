using System.Collections;
using UnityEngine;

public class PlayerMovement : Player
{
    [SerializeField] private float _walkSpeed = 5f;
    [SerializeField] private float _gravity = -13f;
    [SerializeField] private float _moveSmothTime = 0.3f;

    [SerializeField] private AnimationCurve _jumpFallOff;
    [SerializeField] private float _jumpMultiplier;   

    private float _velocityY = 0;

    private bool _isJumping;

    private Vector2 _currentDirection = Vector2.zero;
    private Vector2 _currentDirectionVelocity = Vector2.zero;

    private CharacterController controller;

    protected override void Awake()
    {
        base.Awake();
        _playerInput.Player.Jump.performed += ctx => JumpCheck();
    }

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 targetDirection = _playerInput.Player.Move.ReadValue<Vector2>();
        //targetDirection.Normalize();

        _currentDirection = Vector2.SmoothDamp(_currentDirection, targetDirection, ref _currentDirectionVelocity, _moveSmothTime);

        if (controller.isGrounded)
        {
            _velocityY = 0.0f;
        }

        _velocityY += _gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * _currentDirection.y + transform.right * _currentDirection.x) * _walkSpeed + Vector3.up * _velocityY;

        controller.Move(velocity * Time.deltaTime);
    }

    private void JumpCheck()
    {
        if (_isJumping == false)
        {
            _isJumping = true;
            StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        float timeInAir = 0;
        do
        {
            float jumpForce = _jumpFallOff.Evaluate(timeInAir);
            controller.Move(Vector3.up * jumpForce * _jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;
            yield return null;
        } while (!controller.isGrounded && controller.collisionFlags != CollisionFlags.Above);

        _isJumping = false;
    }
}
