using UnityEngine;

namespace AimStates.States
{
    public class AimingState : AimingBaseState
    {
        private static readonly int IsAiming = Animator.StringToHash("IsAiming");

        public override void EnterState(CameraController aim)
        {
            aim.animator.SetBool(IsAiming, true);
        }

        public override void UpdateState(CameraController aim)
        {
            if (Input.GetKeyUp(KeyCode.Mouse1)) aim.SwitchStates(aim.Shoot);
        }
    }
}
