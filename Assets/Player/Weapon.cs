using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float rayRange = 100f;

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
        Physics.Raycast(this.FPCamera.transform.position, 
                        this.FPCamera.transform.forward, 
                        out hit, 
                        this.rayRange);
        Debug.Log("Pew Pew " + hit.transform.name);
    }
}
