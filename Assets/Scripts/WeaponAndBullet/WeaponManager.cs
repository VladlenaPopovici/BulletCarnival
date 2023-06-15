using ActionStates;
using AimStates;
using Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponAndBullet
{
    public class WeaponManager : MonoBehaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] private bool semiAuto;

        private float _fireRateTimer;

        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform barrelPosition;
        [SerializeField] private float bulletVelocity;
        [SerializeField] private int bulletPerShot;
        public float damage = 20f;
        private CameraController _aim;

        private WeaponAmmo _weaponAmmo;
        private ActionStateManager _actions;
        private WeaponRecoil _weaponRecoil;

        private Light _muzzleFlashLight;
        private ParticleSystem _muzzleFlashParticles;
        private float _lightIntensity;
        [SerializeField] private float lightReturnSpeed = 20;

        private EnemyManager _enemyManager;
    
        void Start()
        {
            _weaponRecoil = GetComponent<WeaponRecoil>();
            _aim = GetComponentInParent<CameraController>();
            _weaponAmmo = GetComponent<WeaponAmmo>();
            _actions = GetComponentInParent<ActionStateManager>();
            _muzzleFlashLight = GetComponentInChildren<Light>();
            _lightIntensity = _muzzleFlashLight.intensity;
            _muzzleFlashLight.intensity = 0;
            _muzzleFlashParticles = GetComponentInChildren<ParticleSystem>();
        
            _fireRateTimer = fireRate;
        }

        void Update()
        {
            if(ShouldFire()) Fire();

            _muzzleFlashLight.intensity = Mathf.Lerp(_muzzleFlashLight.intensity, 0, lightReturnSpeed * Time.deltaTime);
        }

        private bool ShouldFire()
        {
            _fireRateTimer += Time.deltaTime;
            if (_fireRateTimer < fireRate) return false;
            if (_weaponAmmo.currentAmmo == 0) return false;
            if (_actions.CurrentState == _actions.ReloadState) return false;
            if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
            if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
            return false;
        }

        private void Fire()
        {
            _fireRateTimer = 0;
            barrelPosition.LookAt(_aim.aimPosition);
            _weaponRecoil.TriggerRecoil();
            TriggerMuzzleFlash();
            _weaponAmmo.currentAmmo--;

            if (Physics.Raycast(barrelPosition.position, transform.forward, out RaycastHit hit, 100))
            {
                _enemyManager = hit.transform.GetComponent<EnemyManager>();
                if (_enemyManager != null)
                {
                    _enemyManager.Hit(damage);
                }
                for (int i = 0; i < bulletPerShot; i++)
                {
                    var currentBullet = Instantiate(bulletPrefab, barrelPosition.position, barrelPosition.rotation);

                    var bulletScript = currentBullet.GetComponent<Bullet>();
                    bulletScript.weapon = this;
                
                    var rigidBody = currentBullet.GetComponent<Rigidbody>();
                    rigidBody.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
                }
            }
        }

        private void TriggerMuzzleFlash()
        {
            _muzzleFlashParticles.Play();
            _muzzleFlashLight.intensity = _lightIntensity;
        }
    }
}
