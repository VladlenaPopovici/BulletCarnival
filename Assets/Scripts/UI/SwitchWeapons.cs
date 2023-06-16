using System;
using UnityEngine;

namespace UI
{
    public class SwitchWeapons : MonoBehaviour
    {
        public int selectedWeapon;
        public GameObject activeWeapon;

        private void Start()
        {
            SelectWeapon();
        }

        private void Update()
        {
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

            if (previousSelectedWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
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
    }
}