using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] AmmoPickup[] ammoPickups;
    [SerializeField] float randomRespawn;
    [SerializeField] float minRespawn = 15f;
    [SerializeField] float maxRespawn = 30f;

    private static readonly System.Random random = new System.Random();

    AmmoType ammoType;
    BoxCollider boxCollider;

    private void Start()
    {
        this.boxCollider = gameObject.GetComponent<BoxCollider>();
        this.ammoType = RandomAmmoType(random.Next(4));

        SetRandomAmmo();
    }

    private void OnTriggerEnter(Collider other) 
    {
        StartCoroutine(Respawn());
    }

    private AmmoType RandomAmmoType(int i)
    {
        switch (i)
        {
            case 0:
                return AmmoType.Assault;
            
            case 1:
                return AmmoType.Pistol;
            
            case 3:
                return AmmoType.Shotgun;

            default:
                return AmmoType.Sniper;
        }
    }

    private void SetRandomAmmo()
    {
        foreach (AmmoPickup pickup in ammoPickups)
        {   
            if (pickup.AmmoType == this.ammoType && !pickup.isActiveAndEnabled)
            {
                pickup.SetSpawnPosition(this.transform.position);
                break;
            }
        }
    }

    IEnumerator Respawn()
    {
        this.randomRespawn = RandomTime(random.NextDouble());
        this.boxCollider.enabled = false;
        this.ammoType = RandomAmmoType(random.Next(4));
        yield return new WaitForSeconds(this.randomRespawn);
        SetRandomAmmo();
        this.boxCollider.enabled = true;
    }

    private float RandomTime(double randomScale)
    {
        double range = (double) this.minRespawn - (double) this.maxRespawn;
        double scaled = Math.Abs((randomScale * range) + this.minRespawn);
        return (float) scaled;
    }
}
