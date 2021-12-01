using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float rayRange = 100f;
    [SerializeField] float bulletDamage = 25f;

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Camera FPCamera;
    [SerializeField] Ammo ammoSlot;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(this.ammoSlot.AmmoAmount == 0) { return; }

        PlayMuzzleFlash();
        ProcessRaycast();
    }

    private void PlayMuzzleFlash()
    {
        this.muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;
        this.ammoSlot.DecreaseAmmo();

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
