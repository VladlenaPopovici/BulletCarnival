using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ammo; 
        [SerializeField] private GameObject[] weaponIndicator = new GameObject[3];

        private void Start()
        {
            foreach (var t in weaponIndicator)
                t.gameObject.SetActive(true);
        }

        public void SetAmmo(string amount)
        {
            ammo.text = amount;
        }

        public void SetWeaponToDisplay(int current)
        {
            foreach (var t in weaponIndicator)
                t.gameObject.SetActive(true);

            for (var i = 0; i < weaponIndicator.Length; i++)
            {
                if (current == i)
                {
                    weaponIndicator[i].gameObject.SetActive(true);
                }
            }
        }
    }
}
