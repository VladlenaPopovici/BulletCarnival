using UnityEngine;

namespace MovementStates.States
{
    public class RunningState : MovementBasicStates
    {
        private static readonly int IsRunningPistol = Animator.StringToHash("IsRunningPistol");

        public override void EnterState(PlayerController movement)
        {
            movement.animator.SetBool(IsRunningPistol, true);
        }

        public override void UpdateState(PlayerController movement)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walking);
            else if (movement.movement.magnitude < 0.1f) ExitState(movement, movement.Idle);
        }
        
        private void ExitState(PlayerController movement, MovementBasicStates state)
        {
            movement.animator.SetBool(IsRunningPistol, false);
            movement.SwitchStates(state);
        }
    }
}
