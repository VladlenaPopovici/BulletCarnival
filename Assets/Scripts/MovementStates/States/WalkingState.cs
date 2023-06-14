using UnityEngine;

namespace MovementStates.States
{
    public class WalkingState : MovementBasicStates
    {
        private static readonly int IsWalkingPistol = Animator.StringToHash("IsWalkingPistol");

        public override void EnterState(PlayerController movement)
        {
            movement.animator.SetBool(IsWalkingPistol, true);
        }

        public override void UpdateState(PlayerController movement)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) ExitState(movement, movement.Running);
            else if (movement.movement.magnitude < 0.1f) ExitState(movement, movement.Idle);
        }

        private void ExitState(PlayerController movement, MovementBasicStates state)
        {
            movement.animator.SetBool(IsWalkingPistol, false);
            movement.SwitchStates(state);
        }
    }
}