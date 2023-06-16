using UnityEngine;

namespace WeaponAndBullet
{
    public class SwitchWeapons : MonoBehaviour
    {
        [HideInInspector] public int selectedWeapon;
        [SerializeField] private Animator animator;

        private readonly WeaponImagePair[] _weaponsImages = new WeaponImagePair[3]; // TODO change to list

        private void Start()
        {
            InitWeaponsAndImages();
            SelectWeapon();
        }

        private void InitWeaponsAndImages()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var weapon = transform.GetChild(i).gameObject;
                _weaponsImages[i].Weapon = weapon;
                _weaponsImages[i].Sprite = weapon.GetComponent<WeaponManager>().icon;
            }
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
        }

        private void SelectWeapon()
        {
            for (var i = 0; i < _weaponsImages.Length; i++)
            {
                var weapon = _weaponsImages[i].Weapon.gameObject;
                weapon.SetActive(i == selectedWeapon);
                var image = _weaponsImages[i].Sprite.gameObject;
                image.SetActive(i == selectedWeapon);
            }
        }

        private bool ReloadAnimationInProgress()
        {
            return animator.GetCurrentAnimatorStateInfo(1).IsName("RIfle Reload");
        }
    }

    public struct WeaponImagePair
    {
        internal GameObject Weapon;
        internal GameObject Sprite;
    }
}