using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] float rayRange;
    [SerializeField] float bulletDamage;
    [SerializeField] float numOfBullets;
    [SerializeField] float effectiveRange;
    [SerializeField] float deviationAtEffectiveRange = 1f;
    [SerializeField] float rateOfFire;
    [SerializeField] float reloadTime;
    public float ReloadTime { get { return this.reloadTime; } }

    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Camera FPCamera;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    public AmmoType AmmoType { get { return this.ammoType; } }

    bool canShoot = true;
    public void CanShoot(bool b) { this.canShoot = b; }

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
            StartCoroutine(this.ammoSlot.Reload(this));
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
            if (this.ammoSlot.IsReloading) { yield break; }
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

        for (int i = 0; i < this.numOfBullets; i++)
        {
            Vector3 deviation = ProcessShotVariance();

            if (Physics.Raycast(this.FPCamera.transform.position,
                                deviation,
                                out hit,
                                this.rayRange))
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target != null)
                {
                    float modifiedDamage = ProcessRangeDamage(hit);
                    target.TakeDamage(modifiedDamage);
                    Debug.Log(modifiedDamage);
                }   
            }
        }
    }

    private Vector3 ProcessShotVariance()
    {
        Vector3 deviation3D = UnityEngine.Random.insideUnitCircle * this.deviationAtEffectiveRange;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward * this.effectiveRange + deviation3D);
        Vector3 newForward = this.FPCamera.transform.rotation * rot * Vector3.forward;
        return newForward;
    }

    private float ProcessRangeDamage(RaycastHit hit)
    {
        if (hit.distance <= this.effectiveRange)
        {
            return this.bulletDamage;
        }
        else
        {
            float effectiveOffset = this.effectiveRange / hit.distance;
            float newDamage = (float)Math.Round(this.bulletDamage * effectiveOffset);
            return newDamage;
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(this.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 0.1f);
    }
}
