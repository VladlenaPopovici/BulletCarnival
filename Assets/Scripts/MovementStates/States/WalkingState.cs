using UnityEngine;

namespace MovementStates.States
{
    public class WalkingState : MovementBasicStates
    {
        private static readonly int IsWalking = Animator.StringToHash("IsWalking");

        public override void EnterState(PlayerController movement)
        {
            movement.animator.SetBool(IsWalking, true);
        }

        public override void UpdateState(PlayerController movement)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift)) ExitState(movement, movement.Running);
            else if (movement.movement.magnitude < 0.1f) ExitState(movement, movement.Idle);
        }

        private void ExitState(PlayerController movement, MovementBasicStates state)
        {
            movement.animator.SetBool(IsWalking, false);
            movement.SwitchStates(state);
        }
    }
}