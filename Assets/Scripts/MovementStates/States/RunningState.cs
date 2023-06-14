using UnityEngine;

namespace MovementStates.States
{
    public class RunningState : MovementBasicStates
    {
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");

        public override void EnterState(PlayerController movement)
        {
            movement.animator.SetBool(IsRunning, true);
        }

        public override void UpdateState(PlayerController movement)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walking);
            else if (movement.movement.sqrMagnitude < 0.1f) ExitState(movement, movement.Idle);
        }
        
        private void ExitState(PlayerController movement, MovementBasicStates state)
        {
            movement.animator.SetBool(IsRunning, false);
            movement.SwitchStates(state);
        }
    }
}
