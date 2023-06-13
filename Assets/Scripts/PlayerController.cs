using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _runSpeedMultiplier = 2f;
    [SerializeField] private float _jumpForce = 3f;

    private bool _isJumping;
    private bool _isRunning;
    private CharacterController _characterController;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleRunInput();
    }

    private void HandleRunInput()
    {
        
    }

    private void HandleJumpInput()
    {
        
    }

    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput) * (_moveSpeed * Time.deltaTime);

        _characterController.Move(direction);
    }
}
