using InventoryScripts;
using UnityEngine;

namespace WeaponAndBullet
{
    public class WeaponAmmo : MonoBehaviour
    {
        public Gun gunData;
        public int extraAmmo;

        [HideInInspector] public int currentAmmo;
    
        void Start()
        {
            currentAmmo = gunData.maxClipSize;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) Reload();
        }

        private void Reload()
        {
            if (extraAmmo >= gunData.maxClipSize)
            {
                var ammoToReload = gunData.maxClipSize - currentAmmo;
                extraAmmo -= ammoToReload;
                currentAmmo += ammoToReload;
            }
            else if (extraAmmo > 0)
            {
                if (extraAmmo + currentAmmo > gunData.maxClipSize)
                {
                    var leftOverAmmo = extraAmmo - currentAmmo + gunData.maxClipSize;
                    extraAmmo = leftOverAmmo;
                    currentAmmo = gunData.maxClipSize;
                }
                else
                {
                    currentAmmo += extraAmmo;
                    extraAmmo = 0;
                }
            }
        }
    }
}
