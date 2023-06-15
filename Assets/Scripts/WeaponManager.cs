using System.Collections;
using System.Collections.Generic;
using AimStates;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private bool semiAuto;

    private float _fireRateTimer;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform barrelPosition;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private int bulletPerShot;
    private CameraController _aim;
    
    void Start()
    {
        _aim = GetComponentInParent<CameraController>();
        _fireRateTimer = fireRate;
    }

    void Update()
    {
        if(ShouldFire()) Fire();
    }

    private bool ShouldFire()
    {
        _fireRateTimer += Time.deltaTime;
        if (_fireRateTimer < fireRate) return false;
        if (semiAuto && Input.GetKeyDown(KeyCode.Mouse0)) return true;
        if (!semiAuto && Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    private void Fire()
    {
        _fireRateTimer = 0;
        barrelPosition.LookAt(_aim.aimPosition);

        for (int i = 0; i < bulletPerShot; i++)
        {
            var currentBullet = Instantiate(bulletPrefab, barrelPosition.position, barrelPosition.rotation);
            var rigidBody = currentBullet.GetComponent<Rigidbody>();
            rigidBody.AddForce(barrelPosition.forward * bulletVelocity, ForceMode.Impulse);
        }
    }
}
