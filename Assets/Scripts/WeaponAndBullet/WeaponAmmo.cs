using InventoryScripts;
using UnityEngine;

namespace WeaponAndBullet
{
    public class WeaponAmmo : MonoBehaviour
    {
        public Gun gunData;
        public Canvas inventoryCanvas;
        private Inventory _inventory;
        public ItemData weaponBulletType;

        [HideInInspector] public int currentAmmo;
    
        void Start()
        {
            _inventory = inventoryCanvas.GetComponent<Inventory>();
            currentAmmo = gunData.maxClipSize;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R)) Reload();
        }

        private void Reload()
        {
            var extraAmmo = _inventory.Get(weaponBulletType).stackSize;
            if (extraAmmo >= gunData.maxClipSize)
            {
                var ammoToReload = gunData.maxClipSize - currentAmmo;
                
                _inventory.Remove(weaponBulletType, ammoToReload);
                currentAmmo += ammoToReload;
            }
            else if (extraAmmo > 0)
            {
                if (extraAmmo + currentAmmo > gunData.maxClipSize)
                {
                    var leftOverAmmo = extraAmmo - currentAmmo + gunData.maxClipSize;
                    _inventory.Set(weaponBulletType, leftOverAmmo);
                    currentAmmo = gunData.maxClipSize;
                }
                else
                {
                    currentAmmo += extraAmmo;
                    
                    _inventory.Set(weaponBulletType, 0);
                }
            }
        }
    
        public int GetExtraAmmo()
        {
            // return _inventory.Get(weaponBulletType).stackSize;
            return _inventory != null ? _inventory.Get(weaponBulletType)?.stackSize ?? 0 : 0;
        }
    }
}
