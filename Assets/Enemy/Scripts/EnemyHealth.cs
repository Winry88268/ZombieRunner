using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 200f;

    bool isDead = false;
    public bool IsDead { get { return this.isDead; } }

    public void TakeDamage(float damage)
    {
        if (this.isDead) { return; }

        this.hitPoints -= Mathf.Abs(damage);
        
        if(this.hitPoints <= 0)
        {
            Die();
        }
        else
        {
            BroadcastMessage("OnDamage");
        }
    }

    private void Die()
    {
        this.isDead = true;
        GetComponent<Animator>().SetTrigger("death");
    }
}
