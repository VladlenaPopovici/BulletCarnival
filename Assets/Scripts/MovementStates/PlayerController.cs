using MovementStates.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovementStates
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private float groundYOffset;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float runSpeedMultiplier = 2f;

        private CharacterController _characterController;
        [HideInInspector] public Vector3 movement;
        private Vector3 _spherePosition;
        private Vector3 _velocity;
        private bool _isRunning;
        private float _horizontalInput;
        private float _verticalInput;

        private MovementBasicStates _currentState;

        public readonly IdleState Idle = new();
        public readonly WalkingState Walking = new();
        public readonly RunningState Running = new();

        [HideInInspector] public Animator animator;
        private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        private static readonly int VerticalInput = Animator.StringToHash("VerticalInput");

        void Start()
        {
            _characterController = GetComponent<CharacterController>();
            animator = GetComponent<Animator>();
            SwitchStates(Idle);
        }

        void Update()
        {
            HandleMovementInput();
            // HandleJumpInput();
            HandleRunInput();

            ApplyGravity();
            
            animator.SetFloat(HorizontalInput, _horizontalInput);
            animator.SetFloat(VerticalInput, _verticalInput);
            
            _currentState.UpdateState(this);
        }

        public void SwitchStates(MovementBasicStates state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        private void HandleMovementInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            movement = transform.forward * _verticalInput + transform.right * _horizontalInput;

            _characterController.Move(movement.normalized * (moveSpeed * Time.deltaTime));
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
}