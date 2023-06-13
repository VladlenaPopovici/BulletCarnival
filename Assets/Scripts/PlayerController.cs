using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundYOffset;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float runSpeedMultiplier = 2f;

    private CharacterController _characterController;
    private Vector3 _movement;
    private Vector3 _spherePosition;
    private Vector3 _velocity;
    private bool _isRunning;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleRunInput();

        ApplyGravity();
    }

    private void HandleMovementInput()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");

        _movement = transform.forward * verticalInput + transform.right * horizontalInput;
        // FIXME diagonal speed

        _characterController.Move(_movement * (moveSpeed * Time.deltaTime));
    }

    private void HandleJumpInput()
    {
        if (!Input.GetKeyDown(KeyCode.Space) || !IsGrounded()) return;

        _velocity.y += jumpForce;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void HandleRunInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed *= runSpeedMultiplier;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed /= runSpeedMultiplier;
        }
    }

    private void ApplyGravity()
    {
        if (!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
        else if (_velocity.y < 0) _velocity.y = -2f;

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        var position = transform.position;
        _spherePosition = new Vector3(position.x, position.y - groundYOffset, position.z);
        return Physics.CheckSphere(_spherePosition, _characterController.radius - 0.05f, groundMask);
    }
}