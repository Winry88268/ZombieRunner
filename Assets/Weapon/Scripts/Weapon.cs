using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float rayRange;
    [SerializeField] float bulletDamage;
    [SerializeField] float rateOfFire;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Camera FPCamera;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;

    bool canShoot = true;

    void OnEnable() 
    {
        this.canShoot = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && this.canShoot == true)
        {
            StartCoroutine(Shoot());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            this.ammoSlot.Reload(this.ammoType);
        }
    }

    IEnumerator Shoot()
    {
        this.canShoot = false;
        if (this.ammoSlot.GetAmmoLoadedAmount(this.ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast();
            yield return new WaitForSeconds(this.rateOfFire);
        }
        this.canShoot = true;        
    }

    private void PlayMuzzleFlash()
    {
        this.muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        this.ammoSlot.DecreaseAmmo(this.ammoType);

        if (Physics.Raycast(this.FPCamera.transform.position,
                            this.FPCamera.transform.forward,
                            out hit,
                            this.rayRange))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                return;
            }

            
            target.TakeDamage(this.bulletDamage);
        }
        else
        {
            return;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(this.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
