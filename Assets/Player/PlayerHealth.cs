using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void ProcessHit(float damage)
    {
        this.hitPoints -= Mathf.Abs(damage);
        
        if(this.hitPoints <= 0)
        {
            this.GetComponent<DeathHandler>().ProcessDeath();
        }
    }
}
