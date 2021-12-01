using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 40f;

    PlayerHealth target;

    void Start()
    {
        this.target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if(target == null) { return; }
        
        this.target.ProcessHit(this.damage);
    }
}
