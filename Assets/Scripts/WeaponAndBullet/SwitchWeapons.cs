using UI;
using UnityEngine;

namespace WeaponAndBullet
{
    public class SwitchWeapons : MonoBehaviour
    {
        [HideInInspector] public int selectedWeapon;
        public GameObject activeWeapon;

        private float _weaponIndicator;
        [SerializeField] private GameObject[] weapons = new GameObject[3];    
        [SerializeField] private Animator animator;

        private void Start()
        {
            SelectWeapon();
            ChangeWeaponUI(0);
        }

        private void Update()
        {
            if (ReloadAnimationInProgress()) return;
            
            var previousSelectedWeapon = selectedWeapon;
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (selectedWeapon <= 0)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }

            if (previousSelectedWeapon == selectedWeapon) return;

            SelectWeapon();
            ChangeWeaponUI(selectedWeapon);
        }

        private void SelectWeapon()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var weapon = transform.GetChild(i).gameObject;

                weapon.SetActive(i == selectedWeapon);
                if (weapon.activeSelf) activeWeapon = weapon;
            }
        }

        private void ChangeWeaponUI(int index)
        {
            foreach (var t in weapons)
            {
                t.gameObject.SetActive(false);
            }
            
            weapons[index].gameObject.SetActive(true);
            _weaponIndicator = index;
        }

        private bool ReloadAnimationInProgress()
        {
            return animator.GetCurrentAnimatorStateInfo(1).IsName("RIfle Reload");
        }
    }
}