using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Serialization;

namespace ActionStates
{
    public class ActionStateManager : MonoBehaviour
    {
        [HideInInspector] public ActionBaseState CurrentState;
        
        public readonly ReloadState ReloadState = new();
        public readonly DefaultState DefaultState = new();

        public GameObject currentWeapon;
        [HideInInspector] public WeaponAmmo weaponAmmo;

        [HideInInspector] public Animator animator;

        
        void Start()
        {
            SwitchStates(DefaultState);
            weaponAmmo = currentWeapon.GetComponent<WeaponAmmo>();
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            CurrentState.UpdateState(this);
        }

        public void SwitchStates(ActionBaseState state)
        {
            CurrentState = state;
            CurrentState.EnterState(this);
        }

        public void WeaponReloaded()
        {
            weaponAmmo.Reload();
            SwitchStates(DefaultState);
        }
    }
}
