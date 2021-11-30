using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float damage)
    {
        this.hitPoints -= Mathf.Abs(damage);
        
        if(this.hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            BroadcastMessage("OnDamage");
        }
    }
}
