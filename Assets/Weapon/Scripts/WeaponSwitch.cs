using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] int currWeapon = 0;

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = this.currWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();

        if (previousWeapon != this.currWeapon)
        {
            SetWeaponActive();
        }
    }
    
    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            this.currWeapon = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            this.currWeapon = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            this.currWeapon = 2;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            this.currWeapon = 3;
        }
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (this.currWeapon >= this.transform.childCount - 1)
            {
                this.currWeapon = 0;
            }
            else
            {
                this.currWeapon++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (this.currWeapon <= 0)
            {
                this.currWeapon = this.transform.childCount - 1;
            }
            else
            {
                this.currWeapon--;
            }
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in this.transform)
        {
            if (weaponIndex == this.currWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            
            weaponIndex++;
        }
    }
}
