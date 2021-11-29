using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float rayRange = 100f;
    [SerializeField] float bulletDamage = 25f;

    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(this.FPCamera.transform.position, 
                        this.FPCamera.transform.forward, 
                        out hit, 
                        this.rayRange))
        {
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if(target == null)
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
}
