using UnityEngine;

namespace ActionStates
{
    public class DefaultState : ActionBaseState
    {
        public override void EnterState(ActionStateManager actions)
        {

        }

        public override void UpdateState(ActionStateManager actions)
        {
            if (Input.GetKeyDown(KeyCode.R) && CanReload(actions))
            {
                actions.SwitchStates(actions.ReloadState);
            }
        }

        private bool CanReload(ActionStateManager action)
        {
            if (action.weaponAmmo.currentAmmo == action.weaponAmmo.clipSize) return false;
            if (action.weaponAmmo.extraAmmo == 0) return false;
            return true;
        }
    }
}
