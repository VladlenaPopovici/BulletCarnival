using UnityEngine;

namespace ActionStates
{
    public class ReloadState : ActionBaseState
    {
        private static readonly int Reload = Animator.StringToHash("Reload");

        public override void EnterState(ActionStateManager actions)
        {
            actions.animator.SetTrigger(Reload);
        }

        public override void UpdateState(ActionStateManager actions)
        {
            
        }
    }
}
