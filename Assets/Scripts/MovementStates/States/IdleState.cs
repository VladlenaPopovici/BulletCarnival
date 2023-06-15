using UnityEngine;

namespace MovementStates.States
{
    public class IdleState : MovementBasicStates
    {
        public override void EnterState(PlayerController movement)
        {
        }

        public override void UpdateState(PlayerController movement)
        {
            if (!(movement.movement.sqrMagnitude > 0.1f)) return;
            
            if (Input.GetKeyDown(KeyCode.LeftShift)) movement.SwitchStates(movement.Running);
            else movement.SwitchStates(movement.Walking);

            if (!Input.GetKeyDown(KeyCode.Space)) return;
            
            movement.PreviousState = this;
            movement.SwitchStates(movement.Jumping);
        }
    }
}
