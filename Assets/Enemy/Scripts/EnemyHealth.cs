using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 200f;

    public void TakeDamage(float damage)
    {
        this.hitPoints -= Mathf.Abs(damage);
        
        if(this.hitPoints <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            BroadcastMessage("OnDamage");
        }
    }
}
