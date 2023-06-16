using AimStates;
using Enemy;
using InventoryScripts;
using UI;
using UnityEngine;

namespace WeaponAndBullet
{
    public class WeaponManager : MonoBehaviour
    {
        public Gun gunData;

        private float _fireRateTimer;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform barrelPosition;
        [SerializeField] private float bulletVelocity;
        [SerializeField] private int bulletPerShot;
        private CameraController _aim;

        private WeaponAmmo _weaponAmmo;
        private WeaponRecoil _weaponRecoil;

        private Light _muzzleFlashLight;
        private ParticleSystem _muzzleFlashParticles;
        private float _lightIntensity;
        [SerializeField] private float lightReturnSpeed = 20;

        private EnemyManager _enemyManager;

        [SerializeField] private Animator animator;
        private static readonly int Reload1 = Animator.StringToHash("Reload");
        
        private UIManager _uiManager;

        void Start()
        {
            _weaponRecoil = GetComponent<WeaponRecoil>();
            _aim = GetComponentInParent<CameraController>();
            _weaponAmmo = GetComponent<WeaponAmmo>();
            _muzzleFlashLight = GetComponentInChildren<Light>();
            _lightIntensity = _muzzleFlashLight.intensity;
            _muzzleFlashLight.intensity = 0;
            _muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
            animator = GetComponentInParent<Animator>();
            _uiManager = GameObject.FindWithTag("UIWeapon").GetComponent<UIManager>();

            _fireRateTimer = gunData.fireRate;
            
            _uiManager.SetAmmo(_weaponAmmo.currentAmmo + "/" + _weaponAmmo.extraAmmo);
        }

        void Update()
        {
            if(ShouldFire()) Fire();
            _uiManager.SetAmmo(_weaponAmmo.currentAmmo + "/" + _weaponAmmo.extraAmmo);

            _muzzleFlashLight.intensity = Mathf.Lerp(_muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
            
            Reload();
        }

        private bool ShouldFire()
        {
            _fireRateTimer += Time.deltaTime;
            if (_weaponAmmo.currentAmmo == 0) return false;
            if (_fireRateTimer < gunData.fireRate) return false;
            if (ReloadAnimationInProgress()) return false;
            if (gunData.isSemiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
            if (!gunData.isSemiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
            return false;
        }

        private void Fire()
        {
            _fireRateTimer = 0;
            barrelPosition.LookAt(_aim.aimPosition);
            _weaponRecoil.TriggerRecoil();
            TriggerMuzzleFlash();
            _weaponAmmo.currentAmmo--;
            _uiManager.SetAmmo(_weaponAmmo.currentAmmo + "/" + _weaponAmmo.extraAmmo);

            if (!Physics.Raycast(barrelPosition.position, transform.forward, out var hit, 100)) return;

            _enemyManager = hit.transform.GetComponent<EnemyManager>();
            if (_enemyManager != null)
            {
                _enemyManager.Hit(gunData.damage);
            }
            for (var i = 0; i < bulletPerShot; i++)
            {
                var currentBullet = Instantiate(bulletPrefab, barrelPosition.position, barrelPosition.rotation);

                var bulletScript = currentBullet.GetComponent<Bullet>();
                bulletScript.weapon = this;
                
                var rigidBody = currentBullet.GetComponent<Rigidbody>();
                rigidBody.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
            }
        }

        private void TriggerMuzzleFlash()
        {
            _muzzleFlashParticles.Play();
            _muzzleFlashLight.intensity = _lightIntensity;
        }

        private void Reload()
        {
            if (!FullAmmo() && !ReloadAnimationInProgress() && Input.GetKeyDown(KeyCode.R))
            {
                animator.SetTrigger(Reload1);
            }
        }

        private bool FullAmmo()
        {
            return _weaponAmmo.currentAmmo == _weaponAmmo.gunData.maxClipSize;
        }

        private bool ReloadAnimationInProgress()
        {
            return animator.GetCurrentAnimatorStateInfo(1).IsName("RIfle Reload");
        }
    }
}
