using UnityEngine;

namespace MovementStates.States
{
    public class JumpingState : MovementBasicStates
    {
        private static readonly int IdleJump = Animator.StringToHash("IdleJump");
        private static readonly int RunJump = Animator.StringToHash("RunJump");

        public override void EnterState(PlayerController movement)
        {
            if (movement.PreviousState == movement.Idle) movement.animator.SetTrigger(IdleJump);
            else if (movement.PreviousState == movement.Walking || movement.PreviousState == movement.Running)
                movement.animator.SetTrigger(RunJump);
        }

        public override void UpdateState(PlayerController movement)
        {
            if (!movement.hasJumped || !movement.IsGrounded()) return;
            
            movement.hasJumped = false;
            if (movement.horizontalInput == 0 && movement.verticalInput == 0) movement.SwitchStates(movement.Idle);
            else if(Input.GetKeyDown(KeyCode.LeftShift)) movement.SwitchStates(movement.Running);
            else movement.SwitchStates(movement.Walking);
        }
    }
}