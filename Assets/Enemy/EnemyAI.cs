using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float chaseRange = 12f;

    NavMeshAgent navMeshAgent;
    Animator animator;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    void Start()
    {
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();
        this.animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        this.distanceToTarget = Vector3.Distance(target.position, this.transform.position);

        if(isProvoked)
        {
            EngageTarget();
        }
        else if(this.distanceToTarget <= this.chaseRange)
        {
            isProvoked = true;
        }
    }

    private void EngageTarget()
    {
        if(this.distanceToTarget >= this.navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }
        
        if(this.distanceToTarget <= this.navMeshAgent.stoppingDistance)
        {
           AttackTarget();
        }
        else
        {
            this.animator.SetBool("Attack", false);
        }
    }

    private void ChaseTarget()
    {
        this.animator.SetTrigger("Move");
        this.navMeshAgent.SetDestination(this.target.position);
    }
    
    private void AttackTarget()
    {
        this.animator.SetBool("Attack", true);
    }

    void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, this.chaseRange);
    }
}
