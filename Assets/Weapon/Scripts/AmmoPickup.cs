using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    
    [SerializeField] int pickupValue;
    [SerializeField] int upperLimit;
    [SerializeField] AmmoType ammoType;
    public AmmoType AmmoType { get { return ammoType; } }

    private static readonly System.Random random = new System.Random();

    Ammo ammo;

    private void Awake() 
    {
        this.ammo = FindObjectOfType<Ammo>();
    }

    private void OnEnable() 
    {
        this.pickupValue = random.Next(1, this.upperLimit + 1);
    }

    public void SetSpawnPosition(Vector3 spawnPosition)
    {  
        this.transform.position = spawnPosition;
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        this.ammo.IncreaseAmmo(this.pickupValue, this.ammoType);
        this.transform.position = this.transform.parent.position;
        this.gameObject.SetActive(false);      
    }
}
