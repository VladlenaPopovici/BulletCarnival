using System;
using AimStates.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace AimStates
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform cameraFollowPosition;
        [SerializeField] private float mouseSense = 1f;
        private float _xAxis;
        private float _yAxis;

        private AimingBaseState _currentState;
        public AimingState Aiming = new();
        public ShootState Shoot = new();

        [HideInInspector] public Animator animator;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            SwitchStates(Shoot);
        }

        void Update()
        {
            _xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
            _yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;

            _yAxis = Mathf.Clamp(_yAxis, -80, 80);

            _currentState.UpdateState(this);
        }

        private void LateUpdate()
        {
            var localEulerAngles = cameraFollowPosition.localEulerAngles;
            cameraFollowPosition.localEulerAngles = new Vector3(_yAxis, localEulerAngles.y, localEulerAngles.z);
            ;

            var eulerAngles = transform.eulerAngles;
            transform.eulerAngles = new Vector3(eulerAngles.x, _xAxis, eulerAngles.z);
        }

        public void SwitchStates(AimingBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }
    }
}