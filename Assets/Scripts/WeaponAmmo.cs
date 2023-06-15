using System;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    [HideInInspector] public int currentAmmo;
    
    void Start()
    {
        currentAmmo = clipSize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }

    public void Reload()
    {
        if (extraAmmo >= clipSize)
        {
            var ammoToReload = clipSize - currentAmmo;
            extraAmmo -= ammoToReload;
            currentAmmo += ammoToReload;
        }
        else if (extraAmmo > 0)
        {
            if (extraAmmo + currentAmmo > clipSize)
            {
                var leftOverAmmo = extraAmmo - currentAmmo + clipSize;
                extraAmmo = leftOverAmmo;
                currentAmmo = clipSize;
            }
            else
            {
                currentAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
