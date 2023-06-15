using System;
using MovementStates.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovementStates
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 1f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float groundYOffset;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float runSpeedMultiplier = 2f;
        public float airSpeed = 1.5f;

        [HideInInspector] public bool hasJumped;

        private CharacterController _characterController;
        [HideInInspector] public Vector3 movement;
        private Vector3 _spherePosition;
        private Vector3 _velocity;
        private bool _isRunning;
        public float horizontalInput;
        public float verticalInput;

        public MovementBasicStates PreviousState;
        public MovementBasicStates CurrentState;

        public readonly IdleState Idle = new();
        public readonly WalkingState Walking = new();
        public readonly RunningState Running = new();
        public readonly JumpingState Jumping = new();

        [HideInInspector] public Animator animator;
        private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        private static readonly int VerticalInput = Animator.StringToHash("VerticalInput");
        private static readonly int Falling1 = Animator.StringToHash("Falling");

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            SwitchStates(Idle);
        }

        void Update()
        {
            HandleMovementInput();
            HandleRunInput();
            Falling();

            ApplyGravity();
            
            animator.SetFloat(HorizontalInput, horizontalInput);
            animator.SetFloat(VerticalInput, verticalInput);
            
            CurrentState.UpdateState(this);
        }

        public void SwitchStates(MovementBasicStates state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }

        private void HandleMovementInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            
            movement = transform.forward * verticalInput + transform.right * horizontalInput;
            
            _characterController.Move(movement.normalized * (moveSpeed * Time.deltaTime));
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

        public void JumpForce() => _velocity.y += jumpForce;

        public void Jumped() => hasJumped = true;

        private void Falling() => animator.SetBool(Falling1, !IsGrounded());

        private void ApplyGravity()
        {
            if (!IsGrounded()) _velocity.y += gravity * Time.deltaTime;
            else if (_velocity.y < 0) _velocity.y = -2f;

            _characterController.Move(_velocity * Time.deltaTime);
        }

        public bool IsGrounded()
        {
            var position = transform.position;
            _spherePosition = new Vector3(position.x, position.y - groundYOffset, position.z);
            return Physics.CheckSphere(_spherePosition, _characterController.radius - 0.05f, groundMask);

            // Debug.Log($"isGrounded: {checkSphere}");
        }

        // private void OnDrawGizmos()
        // {
        //     var position = transform.position;
        //     var spherePosition = new Vector3(position.x, position.y + _characterController.radius - 0.08f, position.z);
        //     var sphereRadius = _characterController.radius - 0.05f;
        //
        //     // Draw the ground detection sphere
        //     Gizmos.color = Color.cyan;
        //     Gizmos.DrawWireSphere(spherePosition, sphereRadius);
        // }
    }
}