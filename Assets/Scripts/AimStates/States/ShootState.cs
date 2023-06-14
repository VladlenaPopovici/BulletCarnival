using UnityEngine;

namespace AimStates.States
{
    public class ShootState : AimingBaseState
    {
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");

        public override void EnterState(CameraController aim)
        {
            aim.animator.SetBool(IsAiming, false);
            aim.currentFov = aim.shootFov;
        }

        public override void UpdateState(CameraController aim)
        {
            if (Input.GetKey(KeyCode.Mouse1)) aim.SwitchStates(aim.Aiming);
        }
    }
}
